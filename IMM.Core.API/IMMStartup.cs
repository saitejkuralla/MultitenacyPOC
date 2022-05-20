using IMM.Core.API.JWT;
using IMM.Core.API.Repository;
using IMM.EntityFrameworkCore.SQL;
using IMM.MultiTenancy;
using IMM.MultiTenancy.ConfigurationStore;
using IMM.MultiTenancy.Data;
using IMM.MultiTenancy.Security;
using IMM.MultiTenancy.TenantResolveContributers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Text;

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
            services.AddTransient<ITenantResolver, TenantResolver>();
            services.AddTransient<ITenantStore, DefaultTenantStore>();
            services.AddTransient<ITenantResolveResultAccessor, HttpContextTenantResolveResultAccessor>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICurrentTenant, CurrentTenant>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddSingleton<ICurrentTenantAccessor>(AsyncLocalCurrentTenantAccessor.Instance);  //check this in aBP

            services.AddTransient<MultiTenancyMiddleware>();

            services.AddDbContext<ApplicationDbContext>(options => { });

            //JWT TEMP
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenService, TokenService>();

            //TODO : load from DB or JSONFILE
            services.Configure<IMMDefaultTenantStoreOptions>(options =>
            {
                options.Tenants = new[]
                {

                     new TenantConfiguration(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), "tenant1",new ConnectionStrings()
                     {
                               { ConnectionStrings.DefaultConnectionStringName, "Server=(localdb)\\mssqllocaldb;Database=IMM_Tenant1;Trusted_Connection=True;MultipleActiveResultSets=true"},
                            {"db1", "tenant1-db1-value"},
                            {"Admin", "tenant1-Admin-value"}


                     }),
                          new TenantConfiguration(new Guid("22223344-5566-7788-99AA-CCCCDDEEFF11"), "tenant2",new ConnectionStrings()
                     {
                          { ConnectionStrings.DefaultConnectionStringName, "Server=(localdb)\\mssqllocaldb;Database=IMM_Tenant2;Trusted_Connection=True;MultipleActiveResultSets=true"},


                     })
       
                        //new TenantConfiguration(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), "tenant1",)
                        //{
                        //    ConnectionStrings =
                        //{
                        //    { ConnectionStrings.DefaultConnectionStringName, "Server=(localdb)\\mssqllocaldb;Database=IMM_Tenant1;Trusted_Connection=True;MultipleActiveResultSets=truee"},
                        //    {"db1", "tenant1-db1-value"},
                        //    {"Admin", "tenant1-Admin-value"}
                        //}

                        //},
                        //new TenantConfiguration(new Guid("22223344-5566-7788-99AA-CCCCDDEEFF11"), "tenant2")
                        //{
                        //                    ConnectionStrings =
                        //{
                        //    { ConnectionStrings.DefaultConnectionStringName, "Server=(localdb)\\mssqllocaldb;Database=IMM_Tenant2;Trusted_Connection=True;MultipleActiveResultSets=truee"},

                        //}
                        //}
                };
            });


            // TO DO Authentication later this will be moved to separate component
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
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
