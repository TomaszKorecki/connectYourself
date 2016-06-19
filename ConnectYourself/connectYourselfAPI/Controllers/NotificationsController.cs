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
    [RoutePrefix("api/Notifications")]
    public class NotificationsController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
	        var userId = User.Identity.GetUserId();
			NotificationRuleService notificationRuleService = new NotificationRuleService();
	        var rules = notificationRuleService.Collection.Find(x => x.AppUserId == userId).ToList();
			return Ok(rules);
        }

		//api/devices/deviceId
		[Authorize]
		[Route("{notificationId}")]
		public IHttpActionResult GetNotificationRuleDetails(string notificationId) {
			var userId = User.Identity.GetUserId();
			NotificationRuleService notificationRuleService = new NotificationRuleService();
			var rule =
				notificationRuleService.Collection.Find(x => x.AppUserId == userId && x.Id == notificationId).FirstOrDefault();

			if (rule != null) {
				return Ok(rule);
			}

			return BadRequest("Notification rule does not exist");
		}

		[Authorize]
		[HttpPost]
		[Route]
		public IHttpActionResult Post(AddNewNotificationRuleModel addNewNotificationRuleModel) {
			var userId = User.Identity.GetUserId();
			NotificationRuleService notificationRuleService = new NotificationRuleService();
			UserDeviceService userDeviceService = new UserDeviceService();

			var sourceDevice =
				userDeviceService.Collection.Find(
					x => x.AppUserId == userId && x.Name == addNewNotificationRuleModel.SourceDeviceName).FirstOrDefault();

			var targetDevice =
				userDeviceService.Collection.Find(
					x => x.AppUserId == userId && x.Name == addNewNotificationRuleModel.TargetDeviceName).FirstOrDefault();

			if (sourceDevice != null && targetDevice != null) {
				var newRule = new NotificationRule() {
					AppUserId = userId,
					Id = ObjectId.GenerateNewId().ToString(),
					SourceDeviceId = sourceDevice.Id,
					SourceDeviceName = sourceDevice.Name,
					TargetDeviceId = targetDevice.Id,
					TargetDeviceName = targetDevice.Name,
					SourceMessage = addNewNotificationRuleModel.SourceMessage,
					TargetMessage = addNewNotificationRuleModel.TargetMessage
				};

				notificationRuleService.Create(newRule);

				return Ok(newRule);
			}

			return BadRequest("Devices  with such name do not exists");
		}

		[Authorize]
		[HttpDelete]
		[Route("{id}")]
		public IHttpActionResult Delete(string id) {
			var userId = User.Identity.GetUserId();
			NotificationRuleService notificationRuleService = new NotificationRuleService();
			notificationRuleService.Delete(id);
			return Ok();
		}

	}
}
