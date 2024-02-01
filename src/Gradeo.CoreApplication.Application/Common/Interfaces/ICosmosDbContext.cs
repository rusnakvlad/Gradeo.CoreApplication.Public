using Microsoft.Azure.Cosmos;

namespace Gradeo.CoreApplication.Application.Common.Interfaces;

public interface ICosmosDbContext
{
    Container GetContainer(string containerName);
}