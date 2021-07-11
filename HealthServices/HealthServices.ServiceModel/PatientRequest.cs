using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel
{
    public class PatientRequest: IReturn<PatientResponse>
    {
        public string Name { get; set; }
        public string SureName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string HealthID { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string NumbersOfContact { get; set; }
    }

    public class PatientResponse
    {
        public bool Success { get; set; }
        public Patient Patient{ get; set; }
    }
}
