using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
	public interface IPersonBusiness 
	{
		PersonVO Create(PersonVO item);
		PersonVO FindById(long id);
		List<PersonVO> FindByName(string firstName, string secondName);
		List<PersonVO> FindAll();
		PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
		PersonVO Update(PersonVO item);
		PersonVO Disable(long id);
		void Delete(long id);
	}
}
