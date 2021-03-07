using Microsoft.AspNetCore.Mvc;
using SolarSystem.Domain.Models;
using System;

namespace SolarSystem.Web.Controllers {
	public class BasicController : Controller {
		protected ObjectResult DefaultCatch(Exception exception) {
			ErrorStatusCode error = null;
			if (exception is ErrorStatusCode) {
				error = exception as ErrorStatusCode;
			}
			else {
				//We should never expose real exceptions, so we will catch all unknown exceptions (DatabaseErrors, Null Errors, Index errors, etc...) and rethrow an UnknownError after log
				Console.WriteLine(exception);
				error = ErrorStatusCode.UnknownError;
			}
			return new ObjectResult(error.Detail) {
				StatusCode = error.HttpStatusCode
			};
		}
	}
}
