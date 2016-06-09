using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace connectYourselfAPI.Utils {
	public class RandomIdGenerator : IIdGenerator {
		private static readonly Random _random = new Random();

		public object GenerateId(object container, object document) {
			var timestamp = DateTime.UtcNow;
			var machine = _random.Next(0, 16777215);
			var pid = (short)_random.Next(0, 32768);
			var increment = _random.Next(0, 16777215);

			return new ObjectId(timestamp, machine, pid, increment);
		}

		public bool IsEmpty(object id) {
			return (id == null || (ObjectId)id == ObjectId.Empty);
		}
	}
}