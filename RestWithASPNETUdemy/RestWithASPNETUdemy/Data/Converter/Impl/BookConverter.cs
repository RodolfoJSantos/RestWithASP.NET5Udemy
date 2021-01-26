using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.VO;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Data.Converter.Impl
{
	public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
	{
		public BookVO Parse(Book origin)
		{
			if (origin == null) return null;

			return new BookVO
			{
				Id = origin.Id,
				Author = origin.Author,
				Title = origin.Title,
				LaunchDate = origin.LaunchDate,
				Price = origin.Price
			};
		}

		public Book Parse(BookVO origin)
		{
			if (origin == null) return null;

			return new Book
			{
				Id = origin.Id,
				Author = origin.Author,
				Title = origin.Title,
				LaunchDate = origin.LaunchDate,
				Price = origin.Price
			};
		}

		public List<Book> Parse(List<BookVO> origin)
		{
			if (origin == null) return null;

			return origin.Select(item => Parse(item)).ToList();
		}
		public List<BookVO> Parse(List<Book> origin)
		{
			if (origin == null) return null;

			return origin.Select(item => Parse(item)).ToList();
		}
	}
}
