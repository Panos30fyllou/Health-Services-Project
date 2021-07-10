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
    public class PatientInfo : Service
    {
        public PatientResponse Post(PatientRequest request)
        {
            string connectionString = "Server=DESKTOP-5M7O03L;Database=HealthServices;Trusted_Connection=True;";
            DatabaseController.Initialize(connectionString);
            var db = DatabaseController.dbFactory.OpenDbConnection();

            return new PatientResponse() { Success = true };
        }
    }
}
