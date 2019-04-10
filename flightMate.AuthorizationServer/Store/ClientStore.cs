using System.Threading.Tasks;
using flightMate.DataAccess.Abstraction;
using IdentityServer4.Models;
using IdentityServer4.Stores;

public class ClientStore : IClientStore
{
    private readonly IRepository repository;

    public ClientStore(IRepository repository)
    {
        this.repository = repository;
    }
    public async Task<Client> FindClientByIdAsync(string clientId)
    {
        return await repository.Single<Client>(x=>x.ClientId == clientId);
    }
}