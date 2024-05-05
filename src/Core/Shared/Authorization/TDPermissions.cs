using System.Collections.ObjectModel;

namespace TD.WebApi.Shared.Authorization;

public static class TDAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class TDResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
}

public static class TDPermissions
{
    private static readonly TDPermission[] _all = new TDPermission[]
    {
        new("View Dashboard", TDAction.View, TDResource.Dashboard),
        new("View Hangfire", TDAction.View, TDResource.Hangfire),
        new("View Users", TDAction.View, TDResource.Users),
        new("Search Users", TDAction.Search, TDResource.Users),
        new("Create Users", TDAction.Create, TDResource.Users),
        new("Update Users", TDAction.Update, TDResource.Users),
        new("Delete Users", TDAction.Delete, TDResource.Users),
        new("Export Users", TDAction.Export, TDResource.Users),
        new("View UserRoles", TDAction.View, TDResource.UserRoles),
        new("Update UserRoles", TDAction.Update, TDResource.UserRoles),
        new("View Roles", TDAction.View, TDResource.Roles),
        new("Create Roles", TDAction.Create, TDResource.Roles),
        new("Update Roles", TDAction.Update, TDResource.Roles),
        new("Delete Roles", TDAction.Delete, TDResource.Roles),
        new("View RoleClaims", TDAction.View, TDResource.RoleClaims),
        new("Update RoleClaims", TDAction.Update, TDResource.RoleClaims),
        new("View Products", TDAction.View, TDResource.Products, IsBasic: true),
        new("Search Products", TDAction.Search, TDResource.Products, IsBasic: true),
        new("Create Products", TDAction.Create, TDResource.Products),
        new("Update Products", TDAction.Update, TDResource.Products),
        new("Delete Products", TDAction.Delete, TDResource.Products),
        new("Export Products", TDAction.Export, TDResource.Products),
        new("View Brands", TDAction.View, TDResource.Brands, IsBasic: true),
        new("Search Brands", TDAction.Search, TDResource.Brands, IsBasic: true),
        new("Create Brands", TDAction.Create, TDResource.Brands),
        new("Update Brands", TDAction.Update, TDResource.Brands),
        new("Delete Brands", TDAction.Delete, TDResource.Brands),
        new("Generate Brands", TDAction.Generate, TDResource.Brands),
        new("Clean Brands", TDAction.Clean, TDResource.Brands),
        new("View Tenants", TDAction.View, TDResource.Tenants, IsRoot: true),
        new("Create Tenants", TDAction.Create, TDResource.Tenants, IsRoot: true),
        new("Update Tenants", TDAction.Update, TDResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", TDAction.UpgradeSubscription, TDResource.Tenants, IsRoot: true)
    };

    public static IReadOnlyList<TDPermission> All { get; } = new ReadOnlyCollection<TDPermission>(_all);
    public static IReadOnlyList<TDPermission> Root { get; } = new ReadOnlyCollection<TDPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<TDPermission> Admin { get; } = new ReadOnlyCollection<TDPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<TDPermission> Basic { get; } = new ReadOnlyCollection<TDPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record TDPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
