using Google.Cloud.Firestore;
using SolarSystem.Domain.DataAccessLayer.Repository;
using SolarSystem.Domain.DataAccessLayer.UnitOfWork.RepoFactory;
using System.Collections.Generic;

namespace SolarSystem.Domain.DataAccessLayer.UnitOfWork {
	public class UnitOfWork : IUnitOfWork {
		private readonly IRepositoryFactory repositoryFactory;
		private readonly Dictionary<string, object> repos = new Dictionary<string, object>();
		public UnitOfWork(FirestoreDb databaseClient) {
			repositoryFactory = new RepositoryFactory(databaseClient);
		}

		public IRepository<TEntity> Repository<TEntity>() where TEntity : class {
			string typeName = typeof(TEntity).Name;
			repos.TryGetValue(typeName, out object repo);
			if (repo == null) {
				repo = repositoryFactory.Create<TEntity>();
				repos.Add(typeName, repo);
			}
			return repo as IRepository<TEntity>;
		}
	}

}
