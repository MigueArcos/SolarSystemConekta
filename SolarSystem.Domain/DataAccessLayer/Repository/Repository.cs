using Google.Cloud.Firestore;
using SolarSystem.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarSystem.Domain.DataAccessLayer.Repository {
	public class Repository<Model> : IRepository<Model> where Model : class {
		protected readonly FirestoreDb databaseClient;
		private readonly string collectionName = typeof(Model).Name;
		protected readonly CollectionReference collection;
		public Repository(FirestoreDb databaseClient) {
			this.databaseClient = databaseClient;
			collection = databaseClient.Collection(collectionName);
		}

		public async Task<bool> ClearAll() {
			QuerySnapshot snapshot = await collection.GetSnapshotAsync();
			IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
			var tasks = new List<Task<WriteResult>>();
			foreach (DocumentSnapshot document in documents) {
				tasks.Add(document.Reference.DeleteAsync());
			}
			await Task.WhenAll(tasks);
			return true;
		}

		public async Task<bool> Create(Dictionary<string, object> dictionary) {
			DocumentReference docRef = collection.Document();
			await docRef.SetAsync(dictionary);
			return true;
		}

		public async Task<Dictionary<string, object>> GetById(string id) {
			Query query = collection.WhereEqualTo("id", id);
			QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
			if (querySnapshot.Documents.Count == 0) throw ErrorStatusCode.EntityNotFound;
			return querySnapshot.Documents[0].ToDictionary();
		}

		public async Task<IList<Dictionary<string, object>>> GetAll() {
			QuerySnapshot snapshot = await collection.GetSnapshotAsync();
			if (snapshot.Documents.Count == 0) throw ErrorStatusCode.EntityNotFound;
			return snapshot.Documents.Select(d => d.ToDictionary()).ToList();
		}
	}
}
