using Microsoft.AspNetCore.Mvc;
using SolarSystem.Domain.Services.Conditions10YearsService;
using System;
using System.Threading.Tasks;

namespace SolarSystem.Web.Controllers {
	public class TasksController : BasicController {
		private readonly IJobCalculateConditions jobCalculateConditions;

		public TasksController(IJobCalculateConditions jobCalculateConditions) {
			this.jobCalculateConditions = jobCalculateConditions;
		}

		[HttpGet("task/populate")]
		public async Task<IActionResult> PopulateDatabase([FromQuery] int? days) {
			try{
				await jobCalculateConditions.UploadConditionsToDatabase(days ?? 365);
				return Ok(new {
					Message = "Se cargaron los datos"
				});
			} catch(Exception e){
				return DefaultCatch(e);
			}
		}
	}
}
