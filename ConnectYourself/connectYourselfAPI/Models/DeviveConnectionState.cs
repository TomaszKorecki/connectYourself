using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connectYourselfAPI.Models {
	public enum DeviveConnectionState {
		NotConnected, Connecting, Device2Server, Server2Device, FullDuplex
	}
}