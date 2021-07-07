using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel.DataObject
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateofAppointment { get; set; }
        public DateTime Now { get; set; }
        public Patient Patient { get; set; }
        public Priority Priority { get; set; }
        public string Reason { get; set; }
        public string[] Examinations { get; set; }
        public int Duration { get; set; }
        public Doctor Doctor { get; set; }

    }
}
