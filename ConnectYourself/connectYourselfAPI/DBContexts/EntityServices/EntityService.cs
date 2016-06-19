using connectYourselfAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts.EntityServices {
	public class EntityService<T> : IEntityService<T> where T : IMongoEntity {

		protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

		public EntityService() {
			MongoConnectionHandler = new MongoConnectionHandler<T>();
		}

		public IMongoCollection<T> Collection => MongoConnectionHandler.MongoCollection;

		public virtual void Create(T entity) {
			entity.Id = ObjectId.GenerateNewId().ToString();
			MongoConnectionHandler.MongoCollection.InsertOne(entity);
		}

		public virtual void Delete(string id) {
			MongoConnectionHandler.MongoCollection.DeleteOne(x => x.Id == id);
		}

		public virtual T GetById(string id) {
			return MongoConnectionHandler.MongoCollection.Find(x => x.Id == id).FirstOrDefault();
		}

		public virtual bool Update(T entity) {
			//MongoConnectionHandler.MongoCollection.FindOneAndUpdate(x => x.Id == entity.Id, new JsonUpdateDefinition<T>(JsonConvert.SerializeObject(entity)));
			//var result = MongoConnectionHandler.MongoCollection.UpdateOne(x => x.Id == entity.Id, new ObjectUpdateDefinition<T>(entity), new UpdateOptions {
			//	IsUpsert = true
			//});

			//return result.IsAcknowledged;
			var result = MongoConnectionHandler.MongoCollection.ReplaceOne(x => x.Id == entity.Id, entity);
			return result.IsAcknowledged;
		}
	}
}