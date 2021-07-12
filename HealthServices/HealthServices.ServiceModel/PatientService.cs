using HealthServices.ServiceModel.DataObject;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel
{
    public class PatientService
    {
        public void Post(Patient patient, int appointmentId)
        {
            string connectionString = "Server=DESKTOP-5M7O03L;Database=HealthServices;Trusted_Connection=True;";
            DatabaseController.Initialize(connectionString);
            var db = DatabaseController.dbFactory.OpenDbConnection();

            db.Insert<Patient>(patient);

            Appointment appointment = db.Single<Appointment>(x => x.Id == appointmentId);
            appointment.PatientId = patient.Id;
            db.Update(appointment);
            db.Close();
        }
    }
}
