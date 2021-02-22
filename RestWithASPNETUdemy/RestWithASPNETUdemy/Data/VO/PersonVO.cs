using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{
	public class PersonVO	
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("first-name")]
		public string FirstName { get; set; }

		[JsonPropertyName("last-name")]
		public string LastName { get; set; }

		[JsonIgnore]
		public string Address { get; set; }

		[JsonPropertyName("gender")]
		public string Gender { get; set; }

		[JsonPropertyName("enabled")]
		public bool Enabled { get; set; }
	}
}
