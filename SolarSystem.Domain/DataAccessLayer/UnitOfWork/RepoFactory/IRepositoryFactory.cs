using SolarSystem.Domain.DataAccessLayer.Repository;

namespace SolarSystem.Domain.DataAccessLayer.UnitOfWork.RepoFactory {
	public interface IRepositoryFactory {
		IRepository<TEntity> Create<TEntity>() where TEntity : class;
	}
}
