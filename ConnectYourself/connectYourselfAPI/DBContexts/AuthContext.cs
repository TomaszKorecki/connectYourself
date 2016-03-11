using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace connectYourselfAPI.DBContexts
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {

    }
}