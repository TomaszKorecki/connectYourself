using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace connectYourselfAPI.Models.DBModels {
	public class DeviceHistoricalState : IMongoEntity {
		[BsonId]
		public string Id { get; set; }
		public string DeviceId { get; set; }
		public string State { get; set; }
		public DateTime StateTransitionDateTime { get; set; }
	}
}