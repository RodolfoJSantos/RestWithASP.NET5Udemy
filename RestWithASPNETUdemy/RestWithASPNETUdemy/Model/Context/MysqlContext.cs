using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
	public class MysqlContext : DbContext
	{
		public MysqlContext(){}

		public MysqlContext(DbContextOptions<MysqlContext> options) : base(options){}

		public DbSet<Person> People { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
