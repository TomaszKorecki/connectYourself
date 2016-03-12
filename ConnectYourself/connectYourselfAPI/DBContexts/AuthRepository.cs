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

            AuthContext context = new AuthContext(client.GetDatabase("").GetCollection<AppUser>("AppUser"));
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

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            //context.Dispose();
            userManager.Dispose();
        }
    }
}