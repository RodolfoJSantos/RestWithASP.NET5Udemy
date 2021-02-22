using RestWithASPNETUdemy.Data.Converter.Impl;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;
using System.Drawing;

namespace RestWithASPNETUdemy.Business.Implementations
{
	public class PersonBusiness : IPersonBusiness
	{
		private readonly IPersonRepository _personRepository;
		private readonly PersonConverter _converter;
		
		public PersonBusiness(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
			_converter = new PersonConverter();
		}

		public PersonVO FindById(long id)
		{
			return _converter.Parse(_personRepository.FindById(id));
		}

		public List<PersonVO> FindByName(string firstName, string secondName)
		{
			List<Person> people = _personRepository.FindByName(firstName, secondName);
			return _converter.Parse(people);
		}

		public List<PersonVO> FindAll()
		{
			var entities = _personRepository.FindAll();
			var result = _converter.Parse(entities);
			return result;
		}

		public PersonVO Create(PersonVO person)
		{
			var entity = _converter.Parse(person);
			entity = _personRepository.Create(entity);
			return _converter.Parse(entity);
		}

		public PersonVO Update(PersonVO person)
		{
			var entity = _converter.Parse(person);
			entity = _personRepository.Update(entity);
			return _converter.Parse(entity);
		}

		public PersonVO Disable(long id)
		{
			var person = _personRepository.Disable(id);
			return _converter.Parse(person);
		}

		public void Delete(long id)
		{
			_personRepository.Delete(id);
		}

		public bool Exists(long id)
		{
			return _personRepository.Exists(id);
		}

		public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
		{
			var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
			var size = (pageSize < 1) ? 10 : pageSize;
			var offset = page > 0 ? (page - 1) * size : 0;

			string query = @"select * from person p where 1 = 1 ";
			if (!string.IsNullOrWhiteSpace(name)) query += $" and p.first_name like '%{name}%' ";
			query += $" order by p.first_name {sort} limit {size} offset {offset}";

			string countQuery = @"select count(*) from person p where 1 = 1";
			if (!string.IsNullOrWhiteSpace(name)) countQuery += $" and p.first_name like '%{name}%'";

			var people = _personRepository.FindWithPagedSearch(query);
			var totalResults = _personRepository.GetCount(countQuery);

			return new PagedSearchVO<PersonVO>
			{
				CurrentPage = page,
				List = _converter.Parse(people),
				PageSize = size,
				SortDirections = sort,
				TotalResults = totalResults
			};
		}
	}
}
