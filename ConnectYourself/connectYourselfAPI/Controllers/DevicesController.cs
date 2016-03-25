using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using connectYourselfAPI.DBContexts;
using connectYourselfAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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

		[Authorize]
		[HttpPost]
		[Route("")]
		public IHttpActionResult AddDevice(AddNewDeviceViewModel addNewDeviceViewModel) {
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
				CacheData = addNewDeviceViewModel.CahceData,
				Name = addNewDeviceViewModel.Name
			};

			try {
				userDeviceService.Create(device);
			}
			catch (Exception e) {
				return InternalServerError(e);
			}

			return Ok(userDeviceService.GetAllUserDevices(userId));
		}
    }
}
