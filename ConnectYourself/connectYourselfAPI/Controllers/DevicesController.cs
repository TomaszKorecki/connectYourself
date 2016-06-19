using System;
using System.Net;
using System.Web.Http;
using connectYourselfAPI.App_Start;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.DBContexts.EntityServices;
using connectYourselfAPI.EventsControllers;
using connectYourselfAPI.EventsControllers.Models;
using connectYourselfAPI.Models;
using connectYourselfAPI.Models.DBModels;
using connectYourselfAPI.Models.ViewModels;
using connectYourselfLib.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Ninject;

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
						OrderByDescending(x => x.StateTransitionDateTime).
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
						OrderByDescending(x => x.MessageDateTime).
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
		[Route("{id}")]
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

		[Authorize]
		[HttpPost]
		[Route("setDeviceState")]
		public IHttpActionResult SetDeviceState(SetDeviceStateData setDeviceStateData) {
			var userId = User.Identity.GetUserId();
			UserDeviceService userDeviceService = new UserDeviceService();

			if (setDeviceStateData == null) {
				return BadRequest("Input is null");
			}

			var device = userDeviceService.GetBySecretKey(setDeviceStateData.SecretKey);

			if (device != null) {
				DeviceHistoricalState deviceHistoricalState = new DeviceHistoricalState() {
					State = setDeviceStateData.DeviceState,
					StateTransitionDateTime = DateTime.Now,
					DeviceId = device.Id
				};

				if (userDeviceService.UpdateDeviceState(device, setDeviceStateData.DeviceState)) {
					var historialStateES = new EntityService<DeviceHistoricalState>();
					historialStateES.Create(deviceHistoricalState);

					IKernel kernel = new StandardKernel(new ConnectYourselfNinjectModule());
					var deviceEventsContainer = kernel.Get<IDevicesEventsContainer>();

					deviceEventsContainer.RegisterDeviceStateChangeEvent(new DeviceStateChangedEvent {
						DeviceId = device.Id,
						DateTime = deviceHistoricalState.StateTransitionDateTime,
						State = deviceHistoricalState.State,
						AppUserId = device.AppUserId
					});

					return Ok();
				} else {
					return BadRequest("Failed to update state device");
				}
			} else {
				return BadRequest("Device does not exists");
			}
		}
	}
}
