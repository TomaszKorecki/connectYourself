using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.EventsControllers.Models {
	public class DeviceStateChangedNotification {
		public string State { get; set; }
		public DateTime DateTime { get; set; }
		public string DeviceName { get; set; }
	}
}