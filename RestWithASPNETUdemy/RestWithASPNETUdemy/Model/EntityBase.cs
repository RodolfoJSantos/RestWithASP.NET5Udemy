using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
	public class EntityBase
	{
		[Column("id")]
		public long Id { get; set; }
	}
}
