using HealthServices.ServiceModel;
using HealthServices.ServiceModel.DataObject;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthServices.ServiceInterface
{
    public class XRayActions :Service
    {
        public XRayResponse Post(XRayRequest request)
        {
            DatabaseController.Initialize();
            return new XRayResponse();
        }
    }
}
