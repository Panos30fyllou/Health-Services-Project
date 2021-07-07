using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel
{
    public class XRayRequest : IReturn<XRayResponse>
    {
        public string FirstName {get;set;}
        public string LastName { get; set; }
        public string AMKA { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        public DateTime SetDate { get; set; }
        public DateTime RecomendedDate { get; set; }
        public 
    }

    public class XRayResponse
    {

    }
}
