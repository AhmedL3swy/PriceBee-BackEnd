namespace PriceComparing.Services
{
    public class JWT
    {
        public string Key { set; get; }
        public string Issuer { set; get; }
        public string Audience { set; get; }
        public double DurationInDays { set; get; }
    }
}
