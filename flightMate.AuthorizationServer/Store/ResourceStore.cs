using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightMate.DataAccess.Abstraction;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

public class ResourceStore : IResourceStore
{
    private readonly IRepository repository;

    public ResourceStore(IRepository repository)
    {
        this.repository = repository;
    }
    public async Task<ApiResource> FindApiResourceAsync(string name)
    {
        return await repository.Single<ApiResource>(x=>x.Name == name);
    }

    public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
    {
        return await repository.Where<ApiResource>(x=>x.Scopes.Any(s=>scopeNames.Contains(s.Name))).ToListAsync();
    }

    public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
    {
        return await Task.Factory.StartNew(()=>
         repository.Where<IdentityResource>(x=>scopeNames.Contains(x.Name)).AsEnumerable());
    }

    public async Task<Resources> GetAllResourcesAsync()
    {
        return await Task.Factory.StartNew(()=> new Resources(GetAllIdentityResources(), GetAllApiResources()));
    }

    private IEnumerable<ApiResource> GetAllApiResources()
    {
        return repository.All<ApiResource>();
    }

    private IEnumerable<IdentityResource> GetAllIdentityResources()
    {
        return repository.All<IdentityResource>();
    }
}