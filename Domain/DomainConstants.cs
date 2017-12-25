using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Domain
{
    public class DomainConstants
    {
        public static string Connectionstring = WebConfigurationManager.ConnectionStrings["DBConnection"].Name;
    }
}
