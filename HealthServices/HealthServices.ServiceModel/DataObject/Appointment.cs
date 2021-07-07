using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel.DataObject
{
    public class Appointment
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime DateofAppointment { get; set; }
        public DateTime SetDate { get; set; }
        public Patient Patient { get; set; }
        public Priority Priority { get; set; }
        public string Reason { get; set; }
        public XRayType XRayType { get; set; } 
        public Doctor Doctor { get; set; }

    }
}
