using IMM.EntityFrameworkCore.SQL.Data;
using IMM.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IMM.EntityFrameworkCore.SQL
{
    public class ApplicationDbContext : DbContext,IMultiTenant
    {
        private readonly ICurrentTenant _currentTenant;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenant currentTenant) : base(options)
        {
            _currentTenant = currentTenant;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }

        public Guid TenantId
        {
            get { return _currentTenant.Id.Value; }
        }

        Guid? IMultiTenant.TenantId => throw new NotImplementedException();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
       
               optionsBuilder.UseSqlServer(_currentTenant.connectionStrings.Default);
   

          
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var navigation = builder.Entity<ProductCategory>()
                                    .Metadata
                                    .FindNavigation(nameof(ProductCategory.Products));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            base.OnModelCreating(builder);
        }
    }
}