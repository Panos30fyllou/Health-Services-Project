using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel
{
    [Route("/UpdateXRay")]
    public class UpdateAppointmentRequest:IReturn<UpdateAppointmentResponse>
    {

    }
    public class UpdateAppointmentResponse
    {
        public bool Success { get; set; }
    }
}
