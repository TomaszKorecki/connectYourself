using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.DBModels {
	public class NotificationRule : IMongoEntity {
		public string Id { get; set; }
		public string AppUserId { get; set; }
		public string SourceDeviceId { get; set; }
		public string TargetDeviceId { get; set; }
		public string SourceDeviceName { get; set; }
		public string TargetDeviceName { get; set; }
		public string SourceMessage { get; set; }
		public string TargetMessage { get; set; }
	}
}