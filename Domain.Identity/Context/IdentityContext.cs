using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Identity.Context
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(): base("DBConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}
