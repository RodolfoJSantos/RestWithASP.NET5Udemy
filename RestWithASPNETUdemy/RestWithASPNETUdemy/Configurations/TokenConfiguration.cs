namespace RestWithASPNETUdemy.Configurations
{
	public class TokenConfiguration
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public string Secret { get; set; } //palavra que vai usar para cifrar
		public int Minutes { get; set; }
		public int DaysToExpiry { get; set; }
	}
}
