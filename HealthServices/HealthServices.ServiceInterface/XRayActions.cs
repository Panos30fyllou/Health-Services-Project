﻿using HealthServices.ServiceModel;
using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Data;

namespace HealthServices.ServiceInterface
{
    public class XRayActions : Service
    {
        public XRayResponse Post(XRayRequest request)
        {
            string connectionString = "Server=DESKTOP-5M7O03L;Database=HealthServices;Trusted_Connection=True;";
            DatabaseController.Initialize(connectionString);
            var db = DatabaseController.dbFactory.OpenDbConnection();

            //Gets a list of all doctors
            List<Doctor> doctors = db.Select<Doctor>();
            //Finds the minimum number of appointments a doctor has
            int min = int.MaxValue;
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Appointments < min)
                    min = doctor.Appointments;
            }

            //Selects the doctor with the less appointments
            Doctor selectedDoctor = db.Single<Doctor>(x => x.Appointments == min);

            //Gets a list of all the appointments
            List<Appointment> appointments = db.Select<Appointment>();

            //Finds if there is an appointment in the db that conflicts with the request
            bool conflict = false;
            Appointment appointmentInConflict = new Appointment();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.DateofAppointment.Equals(request.RecommendedDate))
                {
                    conflict = true;
                    appointmentInConflict = appointment;
                    break;
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
                appointmentToBeMoved.DateofAppointment.AddHours(1);
                foreach (Appointment appointment in appointments)
                {
                    if (appointment.DateofAppointment.Equals(appointmentToBeMoved.DateofAppointment))
                        conflict = true;
                }
            } while (conflict);

            db.Update(appointmentToBeMoved);
            return newAppointment;
        }

        public Appointment AddToNextAvailable(IDbConnection db, XRayRequest request, Doctor selectedDoctor)
        {
            bool conflict;
            do
            {
                conflict = false;
                request.RecommendedDate.AddHours(1);
                List<Appointment> appointments = db.Select<Appointment>(x => x.DoctorId == selectedDoctor.Id);
                foreach (Appointment appointment in appointments)
                {
                    if (appointment.DateofAppointment.Equals(request.RecommendedDate))
                        conflict = true;
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
            appointmentRequested.SetDate = request.DateSent;
            appointmentRequested.XRayType = request.XRayType;
            appointmentRequested.DoctorId = selectedDoctor.Id;

            return appointmentRequested;
        }
    }
}
