using HealthServices.ServiceModel;
using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System;
using System.Collections.Generic;
using System.Data;

namespace HealthServices.ServiceInterface
{
    public class XRayActions : Service
    {
        public XRayResponse Post(XRayRequest request)
        {
            var db = DatabaseController.dbFactory.OpenDbConnection();

            //db.CreateTable<Doctor>();
            Doctor doctor1 = new Doctor() { NumberOfAppointments = 10, Department = "few", Name = "mpampis", Surname = "flou" };
            //db.Insert<Doctor>(doctor1);
            //Gets a list of all doctors
            List<Doctor> doctors = db.Select<Doctor>();
            //Finds the minimum number of appointments a doctor has
            int min = int.MaxValue;
            foreach (Doctor doctor in doctors)
            {
                if (doctor.NumberOfAppointments < min)
                    min = doctor.NumberOfAppointments;
            }

            //Selects the doctor with the less appointments
            Doctor selectedDoctor = db.Single<Doctor>(x => x.NumberOfAppointments == min);

            //Gets a list of all the appointments
            List<Appointment> appointments = db.Select<Appointment>();

            //Finds if there is an appointment in the db that conflicts with the request
            bool conflict = false;
            Appointment appointmentInConflict = new Appointment() { Priority = Priority.Low };
            foreach (Appointment appointment in appointments)
            {
                if (DatesAreEqual(appointment.DateofAppointment, request.RecommendedDate))
                {
                    conflict = true;
                    if (appointment.Priority <= appointmentInConflict.Priority)
                    {
                        request.RecommendedDate = request.RecommendedDate.AddHours(1);
                        appointmentInConflict = appointment;
                    }
                }
            }

            //If there is a conflict, it resolves it first, and then creates an appointment to return
            Appointment appointmentAdded;
            if (conflict)
                appointmentAdded = ResolveConflict(db, appointmentInConflict, request, selectedDoctor);
            else
            {
                Appointment appointment = CreateAppointmentFromRequest(request, selectedDoctor);
                db.Insert<Appointment>(appointment);
                appointmentAdded = appointment;
            }

            db.Close();

            return new XRayResponse()
            {
                Success = true,
                XRayAppointment = appointmentAdded
            };
        }

        public Appointment ResolveConflict(IDbConnection db, Appointment appointment, XRayRequest request, Doctor selectedDoctor) {
            Appointment appointmentForDb;
            if (request.Priority > appointment.Priority)
                appointmentForDb = MoveAppointments(db, appointment, request, selectedDoctor);
            else
                appointmentForDb = AddToNextAvailable(db, request, selectedDoctor);
            return appointmentForDb;
        }       

        public Appointment MoveAppointments(IDbConnection db, Appointment appointmentToBeMoved, XRayRequest request, Doctor selectedDoctor)
        {
            Appointment newAppointment = CreateAppointmentFromRequest(request, selectedDoctor);
            db.Insert<Appointment>(newAppointment);

            List<Appointment> appointments = db.Select<Appointment>(x => x.DoctorId == selectedDoctor.Id);
            bool conflict;
            do
            {
                conflict = false;
                appointmentToBeMoved.DateofAppointment = appointmentToBeMoved.DateofAppointment.AddHours(1);
                foreach (Appointment appointment in appointments)
                {
                    if (DatesAreEqual(appointment.DateofAppointment, appointmentToBeMoved.DateofAppointment))
                        conflict = true;
                }
            } while (conflict);

            db.Update(appointmentToBeMoved);
            return newAppointment;
        }

        public Appointment AddToNextAvailable(IDbConnection db, XRayRequest request, Doctor selectedDoctor)
        {
            bool conflict;
            List<Appointment> appointments = db.Select<Appointment>(x => x.DoctorId == selectedDoctor.Id);
            do
            {
                conflict = false;
                request.RecommendedDate = request.RecommendedDate.AddHours(1);
                foreach (Appointment appointment in appointments)
                {
                    if (DatesAreEqual(appointment.DateofAppointment, request.RecommendedDate))
                    {
                        conflict = true;
                        break;
                    }
                }
            } while (conflict);

            Appointment appointmentAdded = CreateAppointmentFromRequest(request, selectedDoctor);
            db.Insert<Appointment>(appointmentAdded);
            return appointmentAdded;
        }

        public Appointment CreateAppointmentFromRequest(XRayRequest request, Doctor selectedDoctor) {
            Appointment appointmentRequested = new Appointment();
            appointmentRequested.Priority = request.Priority;
            appointmentRequested.Reason = request.Description;
            appointmentRequested.DateofAppointment = request.RecommendedDate;
            appointmentRequested.DateSent = request.DateSent;
            appointmentRequested.XRayType = request.XRayType;
            appointmentRequested.DoctorId = selectedDoctor.Id;

            return appointmentRequested;
        }

        private bool DatesAreEqual(DateTime dt1, DateTime dt2)
        {
            return dt1.Date.Equals(dt2.Date) && dt1.Hour.Equals(dt2.Hour);
        }
        public DeleteAppointmentResponse Delete(DeleteAppointmentRequest request)
        {
            var db = DatabaseController.dbFactory.OpenDbConnection();
            int deletedvalue = db.Delete<Appointment>(x => x.PatientId == 0);
            if(deletedvalue > 0)
            {
                return new DeleteAppointmentResponse() { Success = true };
            }
            else
            {
                return new DeleteAppointmentResponse() { Success = false };
            }
            
        }
    }
}
