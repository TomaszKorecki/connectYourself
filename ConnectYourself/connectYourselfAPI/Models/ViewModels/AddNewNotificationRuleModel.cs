using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.ViewModels {
	public class AddNewNotificationRuleModel {
		public string SourceDeviceName { get; set; }
		public string TargetDeviceName { get; set; }
		public string SourceMessage { get; set; }
		public string TargetMessage { get; set; }
	}
}