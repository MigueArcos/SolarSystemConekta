using Google.Cloud.Firestore;
using SolarSystem.Domain.DataAccessLayer.Repository;

namespace SolarSystem.Domain.DataAccessLayer.UnitOfWork.RepoFactory {
	public class RepositoryFactory : IRepositoryFactory {
		private readonly FirestoreDb databaseClient;

		public RepositoryFactory(FirestoreDb databaseClient) {
			this.databaseClient = databaseClient;
		}

		public IRepository<TEntity> Create<TEntity>() where TEntity : class {
			return new Repository<TEntity>(databaseClient);
		}
	}
}
