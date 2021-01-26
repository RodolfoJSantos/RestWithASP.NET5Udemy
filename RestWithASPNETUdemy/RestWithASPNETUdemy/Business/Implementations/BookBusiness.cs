using RestWithASPNETUdemy.Data.Converter.Impl;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.VO;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementations
{
	public class BookBusiness : IBookBusiness
	{
		private readonly IRepository<Book> _bookRepository;
		private readonly BookConverter converter;

		public BookBusiness(IRepository<Book> bookRepository)
		{
			_bookRepository = bookRepository;
			converter = new BookConverter();
		}

		public BookVO Create(BookVO book)
		{
			var entity = converter.Parse(book);

			entity = _bookRepository.Create(entity);

			return converter.Parse(entity);
		}
		public BookVO Update(BookVO book)
		{
			var entity = converter.Parse(book);

			entity = _bookRepository.Update(entity);

			return converter.Parse(entity);
		}

		public BookVO FindById(long id)
		{
			return converter.Parse(_bookRepository.FindById(id));
		}

		public List<BookVO> FindAll()
		{
			return converter.Parse(_bookRepository.FindAll());
		}
		public void Delete(long id)
		{
			_bookRepository.Delete(id);
		}

		public bool Exists(long id)
		{
			throw new System.NotImplementedException();
		}
	}
}
