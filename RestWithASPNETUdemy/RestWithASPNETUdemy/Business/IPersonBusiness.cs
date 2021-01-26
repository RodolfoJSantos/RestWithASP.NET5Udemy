using RestWithASPNETUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
	public interface IPersonBusiness 
	{
		PersonVO Create(PersonVO item);
		PersonVO FindById(long id);
		List<PersonVO> FindAll();
		PersonVO Update(PersonVO item);
		void Delete(long id);
	}
}
