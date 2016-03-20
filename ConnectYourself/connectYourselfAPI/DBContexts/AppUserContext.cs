using System.Configuration;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public class AppUserContext {
		public static AppUserContext Create() {
			var mongoURL = ConfigurationManager.AppSettings["MongoDBConnectionURL"];
			var mongoDBName = ConfigurationManager.AppSettings["MongoDBDatabaseName"];

			var client = new MongoClient(mongoURL);
			var database = client.GetDatabase(mongoDBName);

			var users = database.GetCollection<AppUser>(AppUser.AppUserTableName, new MongoCollectionSettings() { AssignIdOnInsert = true });

			return new AppUserContext(users);
		}

		public AppUserContext(IMongoCollection<AppUser> users) {
			Users = users;
		}

		public IMongoCollection<AppUser> Users { get; private set; }
	}
}