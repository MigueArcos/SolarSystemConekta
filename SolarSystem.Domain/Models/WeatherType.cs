using System.ComponentModel;

namespace SolarSystem.Domain.Models {
	public enum WeatherType {
		[Description("Sequía")] 
		Drought,
		[Description("Lluvia")]
		Rainy,
		[Description("Clima normal")]
		Normal,
		[Description("Condiciones óptimas")]
		Nice
	}
}
