using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightMate.DataAccess.Abstraction;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;

public class GrantStore : IPersistedGrantStore
{
    private readonly IRepository repository;

    public GrantStore(IRepository repository)
    {
        this.repository = repository;
    }
    public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
    {
        return await repository.Where<PersistedGrant>(x=>x.SubjectId == subjectId).ToListAsync();
    }

    public async Task<PersistedGrant> GetAsync(string key)
    {
        return await repository.Single<PersistedGrant>(x=>x.Key == key);
    }

    public async Task RemoveAllAsync(string subjectId, string clientId)
    {
        await repository.Delete<PersistedGrant>(x=>x.SubjectId == subjectId && x.ClientId == clientId);
    }

    public async Task RemoveAllAsync(string subjectId, string clientId, string type)
    {
        await repository.Delete<PersistedGrant>(x=>x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type);
    }

    public async Task RemoveAsync(string key)
    {
        await repository.Delete<PersistedGrant>(x=>x.Key == key);
    }

    public async Task StoreAsync(PersistedGrant grant)
    {
        await repository.Add<PersistedGrant>(grant);
    }
}