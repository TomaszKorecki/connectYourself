using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.ViewModels {
	public class NotificationRuleViewModel {
		public string SourceDeviceName { get; set; }
		public string TargetDevicName { get; set; }
		public string SourceMessage { get; set; }
		public string TargetMessage { get; set; }
	}
}