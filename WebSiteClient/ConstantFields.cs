using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebSiteClient
{
    public class ConstantFields
    {
        public static string ApiBaseUrl = ConfigurationManager.AppSettings["APIUrl"];

    }
}