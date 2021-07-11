using HealthServices.ServiceModel;
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

            //Se
            Doctor selectedDoctor = db.Single<Doctor>(x => x.Appointments == min);
            List<Appointment> appointments = db.Select<Appointment>();
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

            Appointment appointmentForDb;
            if (conflict)
                appointmentForDb = Conflict(db, appointmentInConflict, request, selectedDoctor);
            else
                appointmentForDb = CreateAppointmentFromRequest(request, selectedDoctor);

            return new XRayResponse()
            {
                Success = true,
                XRayAppointment = appointmentForDb
            };
        }

        public Appointment Conflict(IDbConnection db, Appointment appointment, XRayRequest request, Doctor selectedDoctor) {
            Appointment appointmentForDb;
            if (request.Priority > appointment.Priority)
                appointmentForDb = MoveAppointments(db, appointment, request, selectedDoctor);
            else
            {
                AddToNextAvailable(db, request, selectedDoctor);
                appointmentForDb = CreateAppointmentFromRequest(request, selectedDoctor);
            }
            return appointmentForDb;
        }       

        public Appointment MoveAppointments(IDbConnection db, Appointment appointment, XRayRequest request, Doctor selectedDoctor)
        {
            db.Insert<Appointment>(CreateAppointmentFromRequest(request, selectedDoctor));
            bool conflict;
            do
            {
                conflict = false;
                appointment.DateofAppointment.AddHours(1);
                List<Appointment> appointments = db.Select<Appointment>(x => x.DoctorId == selectedDoctor.Id);
                foreach (Appointment a in appointments)
                {
                    if (a.DateofAppointment.Equals(appointment.DateofAppointment))
                        conflict = true;
                }
            } while (conflict);

            return appointment;
        }

        public void AddToNextAvailable(IDbConnection db, XRayRequest request, Doctor selectedDoctor)
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

            db.Insert<Appointment>(CreateAppointmentFromRequest(request, selectedDoctor));
        }

        public Appointment CreateAppointmentFromRequest(XRayRequest request, Doctor selectedDoctor) {
            Appointment appointmentRequested = new Appointment();
            appointmentRequested.Priority = request.Priority;
            appointmentRequested.Reason = request.Description;
            appointmentRequested.DateofAppointment = request.RecommendedDate;
            appointmentRequested.SetDate = request.SetDate;
            appointmentRequested.XRayType = request.XRayType;
            appointmentRequested.DoctorId = selectedDoctor.Id;

            return appointmentRequested;
        }
    }
}
