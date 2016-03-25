using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public abstract class EntityService<T> : IEntityService<T> where T : IMongoEntity {

		protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

		protected EntityService() {
			MongoConnectionHandler = new MongoConnectionHandler<T>();
		}

		public IMongoCollection<T> Collection => MongoConnectionHandler.MongoCollection;

		public virtual void Create(T entity) {
			MongoConnectionHandler.MongoCollection.InsertOne(entity);
		}

		public virtual void Delete(string id) {
			MongoConnectionHandler.MongoCollection.DeleteOne(x => x.Id == id);
		}

		public virtual T GetById(string id) {
			return MongoConnectionHandler.MongoCollection.Find(x => x.Id == id).FirstOrDefault();
		}

		public virtual void Update(T entity) {
			MongoConnectionHandler.MongoCollection.UpdateOne(x => x.Id == entity.Id, new ObjectUpdateDefinition<T>(entity));
		}
	}
}