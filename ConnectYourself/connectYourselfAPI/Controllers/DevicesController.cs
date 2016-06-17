using System;
using System.Net;
using System.Web.Http;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.Models;
using connectYourselfAPI.Models.DBModels;
using connectYourselfAPI.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace connectYourselfAPI.Controllers
{
    [RoutePrefix("api/Devices")]
    public class DevicesController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
	        var userId = User.Identity.GetUserId();
			UserDeviceService userDeviceService = new UserDeviceService();

			return Ok(userDeviceService.GetAllUserDevices(userId));
        }

		//api/devices/deviceId
		[Authorize]
		[Route("{deviceId}")]
		public IHttpActionResult GetDeviceDetails(string deviceId) {
			var userId = User.Identity.GetUserId();
			UserDeviceService userDeviceService = new UserDeviceService();

			Device device = userDeviceService.GetById(deviceId);

			if (device != null) {
				if (device.AppUserId == userId) {
					DeviceDetailsViewModel deviceDetailsViewModel = new DeviceDetailsViewModel() {
						ConnectionState = device.ConnectionState,
						LastPing = device.LastPing,
						Id = device.Id,
						ActualState = device.ActualState,
						Name = device.Name,
						SecretKey = device.SecretKey
					};

					return Ok(deviceDetailsViewModel);
				}
				return BadRequest("Device does not available");
			}

			return BadRequest("Device does not exist");
		}

		//api/devices/getDeviceStatesHistory
		[Authorize]
		[Route("getDeviceStatesHistory/{deviceId}")]
		public IHttpActionResult GetDeviceStatesHistory(string deviceId, int startIndex = 0, int limit = 30) {
			var userId = User.Identity.GetUserId();
			UserDeviceService userDeviceService = new UserDeviceService();

			Device device = userDeviceService.GetById(deviceId);

			if (device != null) {
				if (device.AppUserId == userId) {
					var deviceStatesHistoryES = new EntityService<DeviceHistoricalState>();
					var deviceStatesHistory = deviceStatesHistoryES.Collection.AsQueryable().
						Where(x => x.DeviceId == deviceId).
						OrderBy(x => x.StateTransitionDateTime).
						Skip(startIndex).
						Take(limit).
						ToList();

					return Ok(deviceStatesHistory);
				}
				return BadRequest("Device does not available");
			}

			return BadRequest("Device does not exist");
		}

		//api/devices/getDeviceMessagesHistory
		[Authorize]
		[Route("getDeviceMessagesHistory/{deviceId}")]
		public IHttpActionResult GetDeviceMessagesHistory(string deviceId, int startIndex = 0, int limit = 30) {
			var userId = User.Identity.GetUserId();
			UserDeviceService userDeviceService = new UserDeviceService();

			Device device = userDeviceService.GetById(deviceId);

			if (device != null) {
				if (device.AppUserId == userId) {
					var deviceMessageHistoryES = new EntityService<DeviceMessage>();
					var deviceStatesHistory = deviceMessageHistoryES.Collection.AsQueryable().
						Where(x => x.DeviceId == deviceId).
						OrderBy(x => x.MessageDateTime).
						Skip(startIndex).
						Take(limit).
						ToList();

					return Ok(deviceStatesHistory);
				}
				return BadRequest("Device does not available");
			}

			return BadRequest("Device does not exist");
		}

		[Authorize]
		[HttpPost]
		[Route]
		public IHttpActionResult Post(AddNewDeviceViewModel addNewDeviceViewModel) {
			var userId = User.Identity.GetUserId();
			UserDeviceService userDeviceService = new UserDeviceService();

			if (addNewDeviceViewModel == null) {
				return BadRequest("Input is null");
			}

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			Device device = new Device() {
				Id = ObjectId.GenerateNewId().ToString(),
				AppUserId = userId,
				CacheData = addNewDeviceViewModel.CacheData.GetValueOrDefault(),
				Name = addNewDeviceViewModel.Name,
				SecretKey = Guid.NewGuid().ToString("n")
			};

			try {
				userDeviceService.Create(device);
			}
			catch (Exception e) {
				return InternalServerError(e);
			}

			return Ok(device);
		}

		[Authorize]
		[HttpDelete]
		public IHttpActionResult Delete(string id) {
			UserDeviceService userDeviceService = new UserDeviceService();

			if (id.IsNullOrWhiteSpace()) {
				return BadRequest("Input is null");
			}

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			try {
				userDeviceService.Delete(id);
				return Ok();
			}
			catch (Exception e) {
				return InternalServerError(e);
			}
		}

		[Authorize]
		[HttpPut]
		[Route]
		public IHttpActionResult Put(ChangeDeviceViewModel device) {
			UserDeviceService userDeviceService = new UserDeviceService();

			if (device == null) {
				return BadRequest("Input is null");
			}

			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			try {
				var oldDevice = userDeviceService.GetById(device.Id);
				oldDevice.CacheData = device.CacheData;
				var result = userDeviceService.Update(oldDevice);
				if (result) {
					return Ok();
				} else {
					return BadRequest("Entity cannot be modified");
				}
			} catch (Exception e) {
				return InternalServerError(e);
			}
		}

	
	}
}
