using HealthServices.ServiceModel;
using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using System.Collections.Generic;

namespace HealthServices.ServiceInterface
{
    public class XRayActions : Service
    {
        public XRayResponse Post(XRayRequest request)
        {
            //DatabaseController.Initialize();
            return new XRayResponse() { Success = true };

            List<Doctor> doctors = db.Select<Doctor>();
            int min = int.MaxValue;
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Appointments < min)
                    min = doctor.Appointments;
            }

            Doctor selectedDoctor = db.Single<Doctor>(x => x.Appointments == min);
            List<Appointment> appointments = db.Select<Appointment>();
            bool conflict = false;
            foreach (Appointment appointment in appointments)
            {
                if (appointment.DateofAppointment.Equals(request.RecommendedDate))
                    conflict = true;
                if (conflict)
                {
                    Conflict(appointment, request, selectedDoctor);
                    break;
                }
            }

        }
        public void Conflict(Appointment appointment, XRayRequest request, Doctor selectedDoctor) {
            if (request.Priority > appointment.Priority)
                MoveAppointments(appointment, request, selectedDoctor);
            else
                AddToNextAvailable(request, selectedDoctor);
        }       

        public void MoveAppointments(Appointment appointment, XRayRequest request, Doctor selectedDoctor)
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

            db.Update(appointment);
        }

        public void AddToNextAvailable(XRayRequest request, Doctor selectedDoctor)
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

        public Appointment CreateAppointmentFromRequest(XRayRequest request, Doctor doctor) {
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
