using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using connectYourselfAPI.Models;
using MongoDB.Driver;

namespace connectYourselfAPI.DBContexts {
	public interface IEntityService<T> where T : IMongoEntity {
		IMongoCollection<T> Collection { get; }
		void Create(T entity);
		void Delete(string id);
		T GetById(string id);
		void Update(T entity);
	}
}
