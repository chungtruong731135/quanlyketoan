using TD.WebApi.Infrastructure.Multitenancy;

namespace TD.WebApi.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbForTenantAsync(TDTenantInfo tenant, CancellationToken cancellationToken);
}