using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNet.Identity.MongoDB;

namespace connectYourselfAPI.Models
{
	public class AppUser : IdentityUser
	{
		public const string AppUserTableName = "app_users";
	}
}