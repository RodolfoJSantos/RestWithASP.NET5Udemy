using RestWithASPNETUdemy.VO;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
	public interface IBookBusiness 
	{
		BookVO Create(BookVO item);
		BookVO FindById(long id);
		List<BookVO> FindAll();
		BookVO Update(BookVO item);
		void Delete(long id);
		bool Exists(long id);
	}
}
