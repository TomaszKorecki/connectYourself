using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public class UserDeviceService : EntityService<Device> {

		public Device GetBySecretKey(string secretKey) {
			return MongoConnectionHandler.MongoCollection.Find(x => x.SecretKey == secretKey).FirstOrDefault();
		}

		public List<Device> GetAllUserDevices(string userId) {
			return Collection.AsQueryable().Where(x => x.AppUserId == userId).ToList();
		}

		public bool UpdateDeviceState(string deviceId, string deviceState) {
			var device = GetById(deviceId);

			if (device != null) {
				device.ActualState = deviceState;

				return Update(device);
			}
			return false;
		}

		public bool UpdateDeviceConnectionState(string deviceId, DeviveConnectionState deviveConnectionState) {
			var device = GetById(deviceId);

			if (device != null) {
				device.ConnectionState = deviveConnectionState;

				return Update(device);
			}
			return false;
		}

		public bool UpdateDeviceLastPing(string deviceId, DateTime lastPing) {
			var device = GetById(deviceId);

			if (device != null) {
				device.LastPing = lastPing;

				return Update(device);
			}
			return false;
		}
	}
}