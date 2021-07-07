using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthServicesProject.Core;
using System.Data.SqlClient;
using System.Data;

namespace Health_Services_Project.Pages.Procedures
{
    public class sendXrayModel : PageModel
    {
        public string ConnectionString { get; set; }
        public void OnPost()
        {
            XRay xRay = new XRay();  
        }
    }
}
