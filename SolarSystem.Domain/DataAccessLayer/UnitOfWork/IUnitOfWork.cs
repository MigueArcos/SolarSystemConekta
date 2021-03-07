using SolarSystem.Domain.DataAccessLayer.Repository;

namespace SolarSystem.Domain.DataAccessLayer.UnitOfWork {
	public interface IUnitOfWork {
		IRepository<TEntity> Repository<TEntity>() where TEntity : class;
	}
}
