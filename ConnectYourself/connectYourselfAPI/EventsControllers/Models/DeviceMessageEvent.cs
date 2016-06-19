﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.EventsControllers.Models {
	public class DeviceMessageEvent {
		public string Message { get; set; }
		public DateTime DateTime { get; set; }
		public string DeviceId { get; set; }
		public string DeviceName { get; set; }
		public string AppUserId { get; set; }
	}
}