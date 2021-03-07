using Google.Cloud.Firestore;

namespace SolarSystem.Domain.DataAccessLayer.Repository {
	public class Repository<Model> : IRepository<Model> where Model : class {
		protected readonly FirestoreDb databaseClient;
		public Repository(FirestoreDb databaseClient) {
			this.databaseClient = databaseClient;
		}


	}
}
