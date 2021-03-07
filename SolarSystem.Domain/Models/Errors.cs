using System;

namespace SolarSystem.Domain.Models {
	public struct ErrorMessages {
		public const string UnknownError = "Error desconocido";

		public const string EntityNotFound = "No se encontro información";

		public const string WeatherNotFoundForDay = "No se encontraron condiciones climáticas para este día";
	}

	public class ErrorDetail {
		public string Message { get; set; }
		// public string Description { get; set; }
	}

	public class ErrorStatusCode : Exception {
		public ErrorDetail Detail { get; set; }
		public int HttpStatusCode { get; set; }
		public ErrorStatusCode(int httpStatusCode, string errorMessage) {
			Detail = new ErrorDetail {
				Message = errorMessage
			};
			HttpStatusCode = httpStatusCode;
		}
		public static readonly ErrorStatusCode UnknownError = new ErrorStatusCode(500, ErrorMessages.UnknownError);

		public static readonly ErrorStatusCode EntityNotFound = new ErrorStatusCode(404, ErrorMessages.EntityNotFound);

		public static readonly ErrorStatusCode WeatherNotFoundForDay = new ErrorStatusCode(404, ErrorMessages.WeatherNotFoundForDay);
	}
}
