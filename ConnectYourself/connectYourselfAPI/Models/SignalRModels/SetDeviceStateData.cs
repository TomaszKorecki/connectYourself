using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models.SignalRModels {
	public class SetDeviceStateData {
		public string DeviceName { get; set; }
		public string SecretKey { get; set; }
		public string DeviceState { get; set; }
	}
}