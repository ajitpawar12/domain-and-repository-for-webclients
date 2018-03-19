using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Identity.Infrastructure
{
    interface IIdentityRepository:IDisposable
    {
        Task<HttpResponseMessage> LogIn(string userId, string password);
    }
}
