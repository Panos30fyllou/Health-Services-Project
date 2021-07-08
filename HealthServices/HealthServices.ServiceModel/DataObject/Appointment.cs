using ServiceStack.DataAnnotations;
using System;

namespace HealthServices.ServiceModel.DataObject
{
    public class Appointment
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime DateofAppointment { get; set; }
        public DateTime SetDate { get; set; }
        public int PatientId { get; set; }
        public Priority Priority { get; set; }
        public string Reason { get; set; }
        public XRayType XRayType { get; set; }
        public int DoctorId { get; set; }

    }
}
