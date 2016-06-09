using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.SignalRModels {
	public class DevicePing {
		public string DeviceName { get; set; }
		public string SecretKey { get; set; }
	}
}