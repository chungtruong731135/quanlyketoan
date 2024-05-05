using Finbuckle.MultiTenant.Stores;
using TD.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TD.WebApi.Infrastructure.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<TDTenantInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TDTenantInfo>().ToTable("Tenants", SchemaNames.MultiTenancy);
    }
}