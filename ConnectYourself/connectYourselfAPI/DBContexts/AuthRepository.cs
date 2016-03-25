using System;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using connectYourselfAPI.Models;
using Microsoft.AspNet.Identity;

namespace connectYourselfAPI.DBContexts {
	public class AuthRepository : IDisposable {
		private UserManager<AppUser> userManager;

		public AuthRepository() {
			AppUserService appUserService = new AppUserService();
			UserStore<AppUser> userStore = new UserStore<AppUser>(appUserService.Collection);
			userManager = new UserManager<AppUser>(userStore);
		}

		public async Task<IdentityResult> RegisterUser(RegisterUserViewModel registerUserViewModel) {
			AppUser user = new AppUser {
				UserName = registerUserViewModel.UserName
			};

			var result = await userManager.CreateAsync(user, registerUserViewModel.Password);
			return result;
		}

		public async Task<AppUser> FindUser(string userName, string password) {
			AppUser user = await userManager.FindAsync(userName, password);
			return user;
		}

		public void Dispose() {
			userManager.Dispose();
		}
	}
}