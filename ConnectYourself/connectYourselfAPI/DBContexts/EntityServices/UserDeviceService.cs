using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.Models;
using connectYourselfAPI.Models.DBModels;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public class UserDeviceService : EntityService<Device> {

		public Device GetBySecretKey(string secretKey) {
			return MongoConnectionHandler.MongoCollection.Find(x => x.SecretKey == secretKey).FirstOrDefault();
		}

		public Device GetByConnectionId(string connectionString) {
			return MongoConnectionHandler.MongoCollection.Find(x => x.ConnectionId == connectionString).FirstOrDefault();
		}

		public List<Device> GetAllUserDevices(string userId) {
			return Collection.AsQueryable().Where(x => x.AppUserId == userId).ToList();
		}

		public bool UpdateDeviceState(Device device, string deviceState) {
			if (device != null) {
				if (Update(device)) {
					device.ActualState = deviceState;

					return Update(device);
				}
			}
			return false;
		}

		public bool UpdateDeviceConnectionState(string secretKey, DeviveConnectionState deviveConnectionState) {
			var device = GetBySecretKey(secretKey);

			if (device != null) {
				device.ConnectionState = deviveConnectionState;

				return Update(device);
			}
			return false;
		}

		public bool UpdateDeviceLastPing(string secretKey, DateTime lastPing) {
			var device = GetBySecretKey(secretKey);

			if (device != null) {
				device.LastPing = lastPing;

				return Update(device);
			}
			return false;
		}
	}
}