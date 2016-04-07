using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace connectYourselfAPI.Controllers
{

	[Route]
	[AllowAnonymous]
	public class DefaultController : ApiController
    {
		[Route]
		[AllowAnonymous]
        // GET: api/Default
        public string Get() {
			return "Server is working properly";
		}

		[Route("api/")]
		[AllowAnonymous]
		// GET: api/Default
		public string GetApi() {
			return "Server api";
		}
	}
}
