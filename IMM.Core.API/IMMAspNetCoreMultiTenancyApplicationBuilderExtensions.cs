namespace IMM.Core.API
{
    public static class AspNetCoreMultiTenancyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<MultiTenancyMiddleware>();
        }
    }
}
