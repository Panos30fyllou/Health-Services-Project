using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel
{
    public class XRayRequest : IReturn<XRayResponse>
    {
        public Priority Priority { get; set; }
        public string Description { get; set; }
        public DateTime SetDate { get; set; }
        public DateTime RecomendedDate { get; set; }
        public XRayType XRayType { get; set; }
    }

    public class XRayResponse
    {
        public bool Success { get; set; }
        public Appointment Appointment { get; set; }
    }
}
