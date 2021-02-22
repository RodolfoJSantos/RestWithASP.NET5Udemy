using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Implementations
{
	public class GenericRepository<T> : IRepository<T> where T : EntityBase
	{
		protected readonly MysqlContext _mysqlContext;
		private readonly DbSet<T> dataSet;
		public GenericRepository(MysqlContext mysqlContext)
		{
			_mysqlContext = mysqlContext;
			dataSet = _mysqlContext.Set<T>();
		}

		public T FindById(long id)
		{
			return dataSet.SingleOrDefault(i => i.Id.Equals(id));
		}

		public List<T> FindAll()
		{
			return dataSet.ToList();
		}

		public T Create(T item)
		{
			try
			{
				dataSet.Add(item);
				_mysqlContext.SaveChanges();
				return item;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public T Update(T item)
		{
			var result = dataSet.SingleOrDefault(i => i.Id.Equals(item.Id));
			if (result != null)
			{
				try
				{
					_mysqlContext.Entry(result).CurrentValues.SetValues(item);
					_mysqlContext.SaveChanges();
					return item;
				}
				catch (Exception)
				{

					throw;
				}
			}
			return result;
		}

		public void Delete(long id)
		{
			var item = dataSet.SingleOrDefault(i => i.Id.Equals(id));
			if (item != null)
			{
				try
				{
					dataSet.Remove(item);
					_mysqlContext.SaveChanges();
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		public bool Exists(long id)
		{
			return dataSet.Any(i => i.Id.Equals(id));
		}

		public List<T> FindWithPagedSearch(string query)
		{
			return dataSet.FromSqlRaw(query).ToList();
		}

		public int GetCount(string query)
		{
			var result = "";
			using (var connection = _mysqlContext.Database.GetDbConnection())
			{
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = query;
					result = command.ExecuteScalar().ToString();
				}
			}
			return int.Parse(result);
		}
	}
}
