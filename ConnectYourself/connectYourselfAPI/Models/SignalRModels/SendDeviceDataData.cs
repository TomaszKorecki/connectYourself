using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.SignalRModels {
	public class SendDeviceDataData {
		public string DeviceName { get; set; }
		public string SecretKey { get; set; }
		public string Data { get; set; }
	}
}