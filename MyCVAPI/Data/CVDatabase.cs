using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using MongoDB.Driver;

namespace MyCVAPI.Data
{
	public class CVDatabase
	{
		private IMongoDatabase db;

		private string connectionstring = Environment.GetEnvironmentVariable("MicaelCVMongoDB",EnvironmentVariableTarget.Machine);

		public CVDatabase(string database)
		{
			var client = new MongoClient(connectionstring);

			db = client.GetDatabase(database);
		}


		//Create
		public async Task AddData<T>(string table, T data)
		{
			var collection = db.GetCollection<T>(table);
			await collection.InsertOneAsync(data);
		}

		//Read
		public async Task<T> GetDataById<T>(string table, T data) where T : IId
		{
			var collection = db.GetCollection<T>(table);

			return await collection.Find(d => d.Id == data.Id).FirstOrDefaultAsync();
		}

		public async Task<T> GetDataByName<T>(string table, string name) where T : IId
		{
			var collection = db.GetCollection<T>(table);

			return await collection.Find(d => d.Name == name).FirstOrDefaultAsync();
		}

		public async Task<List<T>> GetAllData<T>(string table)
		{
			var collection = db.GetCollection<T>(table);

			return await collection.AsQueryable().ToListAsync();
		}

		//Update
		public async Task UpdateData<T>(string table, T data) where T : IId
		{
			var collection = db.GetCollection<T>(table);

			await collection.ReplaceOneAsync(d => d.Id == data.Id, data);
		}

		//Delete
		public async Task DeleteData<T>(string table, T data) where T : IId
		{
			var collection = db.GetCollection<T>(table);

			await collection.DeleteOneAsync(d => d.Id == data.Id);
		}
	}
}
