using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public class MongoConnectionHandler<T> where T : IMongoEntity {

		public IMongoCollection<T> MongoCollection { get; private set; }

		public MongoConnectionHandler() {
			var mongoURL = ConfigurationManager.AppSettings["MongoDBConnectionURL"];
			var mongoDBName = ConfigurationManager.AppSettings["MongoDBDatabaseName"];

			var client = new MongoClient(mongoURL);
			var database = client.GetDatabase(mongoDBName);

			string collectionName = typeof(T).Name.ToLower() + "s";
			MongoCollection = database.GetCollection<T>(collectionName, new MongoCollectionSettings() {AssignIdOnInsert = true});
		}
	}
}