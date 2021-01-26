using RestWithASPNETUdemy.Data.Converter.Impl;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementations
{
	public class PersonBusiness : IPersonBusiness
	{
		private readonly IRepository<Person> _personRepository;
		private readonly PersonConverter _converter;
		
		public PersonBusiness(IRepository<Person> personRepository)
		{
			_personRepository = personRepository;
			_converter = new PersonConverter();
		}

		public PersonVO FindById(long id)
		{
			return _converter.Parse(_personRepository.FindById(id));
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

		public void Delete(long id)
		{
			_personRepository.Delete(id);
		}

		public bool Exists(long id)
		{
			throw new System.NotImplementedException();
		}
	}
}
