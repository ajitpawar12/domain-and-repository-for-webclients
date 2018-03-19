using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using Domain.Identity.Identity;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;

namespace Domain.Identity.Infrastructure
{
    public class IdentityRepository:IIdentityRepository
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> LogIn(string userId, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(userId, password, false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return new HttpResponseMessage(HttpStatusCode.Accepted);

                case SignInStatus.Failure:
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    
                    
            }
        }
    }
}
