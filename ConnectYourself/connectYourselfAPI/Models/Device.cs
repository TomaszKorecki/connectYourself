﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace connectYourselfAPI.Models {
	public class Device : IMongoEntity{

		[BsonId]
		public string Id { get; set; }
		public string AppUserId { get; set; }
		public string Name { get; set; }
		public bool CacheData { get; set; }

		[BsonRepresentation(BsonType.String)]
		public DeviceConnectingMode ConnectingMode { get; set; }

		[BsonRepresentation(BsonType.String)]
		public DeviveConnectionState ConnectionState { get; set; }
	}
}