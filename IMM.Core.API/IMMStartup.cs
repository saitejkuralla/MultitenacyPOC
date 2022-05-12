﻿using IMM.MultiTenancy;
using IMM.MultiTenancy.ConfigurationStore;
using IMM.MultiTenancy.Security;
using IMM.MultiTenancy.TenantResolveContributers;

namespace IMM.Core.API
{
    public class IMMStartup
    {
        public IMMStartup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ITenantConfigurationProvider, TenantConfigurationProvider>();
            services.AddTransient<ITenantResolver,TenantResolver>();
            services.AddTransient<ITenantStore, DefaultTenantStore>();
            services.AddTransient<ITenantResolveResultAccessor, HttpContextTenantResolveResultAccessor>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICurrentTenant, CurrentTenant>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddSingleton<ICurrentTenantAccessor>(AsyncLocalCurrentTenantAccessor.Instance);  //check this in aBP

            services.AddTransient<MultiTenancyMiddleware>();

            //TODO : load from DB or JSONFILE
            services.Configure<AbpDefaultTenantStoreOptions>(options =>
            {
                options.Tenants = new[]
                {
                        new TenantConfiguration(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), "tenant1"),
                        new TenantConfiguration(new Guid("22223344-5566-7788-99AA-CCCCDDEEFF11"), "tenant2")

                };
            });


            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMultiTenancy();

            app.Run();

        }
    }
}