namespace IMM.Core.API
{
    public class APITenantInfo
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public  IEnumerable<WeatherForecast>? WeatherForecast { get; set; }
    }
}
