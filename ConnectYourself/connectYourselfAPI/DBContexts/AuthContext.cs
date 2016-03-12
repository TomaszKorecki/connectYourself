using AspNet.Identity.MongoDB;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts
{
    public class AuthContext 
    {
        public IMongoCollection<AppUser> Users { get; private set; }

        public AuthContext(IMongoCollection<AppUser> users)
        {
            Users = users;
            EnsureUniqueIndexOnUserName(users);
        }

        private void EnsureUniqueIndexOnUserName(IMongoCollection<AppUser> users)
        {
            //var userName = new IndexKeysBuilder().Ascending("UserName");
            //var unique = new IndexOptionsBuilder().SetUnique(true);

            //users.EnsureIndex(userName, unique);
        }
    }
}