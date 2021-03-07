using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Domain.DataAccessLayer.Repository {
	public interface IRepository<Model> where Model : class {
		Task<bool> ClearAll();
		Task<bool> Create(Dictionary<string, object> model);
		Task<Dictionary<string, object>> GetById(string id);
		Task<IList<Dictionary<string, object>>> GetAll();
	}
}
