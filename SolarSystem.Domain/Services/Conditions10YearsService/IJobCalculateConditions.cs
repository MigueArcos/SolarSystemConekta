using System.Threading.Tasks;

namespace SolarSystem.Domain.Services.Conditions10YearsService {
	public interface IJobCalculateConditions {
		Task<bool> UploadConditionsToDatabase(int howManyDays);
	}
}
