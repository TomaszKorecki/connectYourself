using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models {
	public class DeviceMessage : IMongoEntity {
		public string Id { get; }
		public string DeviceId { get; set; }
		public string Channel { get; set; }
		public string Content { get; set; }
	}
}