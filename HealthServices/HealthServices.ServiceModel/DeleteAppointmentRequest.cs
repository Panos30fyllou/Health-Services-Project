using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthServices.ServiceModel
{
    [Route("/DeleteXRay")]
    public class DeleteAppointmentRequest: IReturn<DeleteAppointmentResponse>
    {
        
    }
    public class DeleteAppointmentResponse
    {
        public bool Success { get; set; }
    }
}
