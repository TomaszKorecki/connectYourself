using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace connectYourselfAPI.Models {
	public class Device : IMongoEntity{

		public Device() {
			Id = ObjectId.GenerateNewId().ToString();
		}

		[BsonId]
		public string Id { get; private set; }
		public string AppUserId { get; set; }
		public string Name { get; set; }
		public bool CacheData { get; set; }
	}
}