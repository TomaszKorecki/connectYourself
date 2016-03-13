using System;
using System.Threading.Tasks;
using AspNet.Identity.MongoDB;
using connectYourselfAPI.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts
{
    public class AuthRepository : IDisposable
    {
        private UserManager<AppUser> userManager;

        public AuthRepository() {
            MongoClient client = new MongoClient();

            AppUserContext context = AppUserContext.Create();
            UserStore<AppUser> userStore = new UserStore<AppUser>(context.Users);

            userManager = new UserManager<AppUser>(userStore);
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            AppUser user = new AppUser
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<AppUser> FindUser(string userName, string password)
        {
            AppUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            userManager.Dispose();
        }
    }
}