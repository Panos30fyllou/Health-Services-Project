using System;
using ServiceStack;
using HealthServices.ServiceModel;

namespace HealthServices.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
