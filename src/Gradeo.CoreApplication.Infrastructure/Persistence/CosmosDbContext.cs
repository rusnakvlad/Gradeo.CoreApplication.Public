using Gradeo.CoreApplication.Application.Common.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Gradeo.CoreApplication.Infrastructure.Persistence;

public class CosmosDbContext : ICosmosDbContext
{
    private readonly Database _database;
    
    public Container GetContainer(string containerName)
    {
        return _database.GetContainer(containerName);
    }

    public CosmosDbContext(Database database)
    {
        _database = database;
    }
}