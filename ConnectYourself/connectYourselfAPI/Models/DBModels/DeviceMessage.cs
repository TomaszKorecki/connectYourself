using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace connectYourselfAPI.Models {
	public class DeviceMessage : IMongoEntity {
		[BsonId]
		public string Id { get; set; }
		public string DeviceId { get; set; }
		public string MessageContent { get; set; }
		public DateTime MessageDateTime { get; set; }
	}
}