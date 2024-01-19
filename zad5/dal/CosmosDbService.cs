using Microsoft.Azure.Cosmos;
using zad5.Models;

namespace zad5.dal
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container container;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
           container = cosmosClient.GetContainer(databaseName, containerName);  
        }

        public async Task AddItemAsync(Item item) => await container.CreateItemAsync(item, new PartitionKey(item.Id));

        public async Task UpdateItemAsync(Item item) => await container.UpsertItemAsync(item, new PartitionKey(item.Id));

        public async Task DeleteItemAsync(Item item) => await container.DeleteItemAsync<Item>(item.Id, new PartitionKey(item.Id));

        public async Task<IEnumerable<Item>> GetItemsAsync(string queryString)
        {
            List<Item> items = new(); 
            var query = container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();
                items.AddRange(result.ToList()); 
            }
            return items;
        }

        public async Task<Item?> GetItemAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Item>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return null;
            }
        }
    }
}
