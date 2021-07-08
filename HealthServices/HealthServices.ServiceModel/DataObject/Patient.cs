using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel.DataObject
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string HealthID { get; set; }       
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
        public string[] NumbersOfContact { get; set; }
        
    }
}
