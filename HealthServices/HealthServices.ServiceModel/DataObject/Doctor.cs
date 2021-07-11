using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel.DataObject
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public int NumberOfAppointments { get; set; }
    }
}
