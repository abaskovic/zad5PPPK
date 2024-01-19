using Microsoft.Azure.Cosmos;

namespace zad5.dal
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "Items";
        private const string ContainerName = "Todo";
        private const string Account = "https://dbeletodoitems.documents.azure.com:443/";
        private const string Key = "Ub4C7aePad3cq03fuL15Lp7WmuKhPzvSecm8soU33900U4vs2o4Q3cxuLtQJxrnfEwezkS4d97g3ACDbiRhzQw==";

        private static ICosmosDbService? service;

        public static ICosmosDbService? Service { get => service; }

        public async static Task Init() 
        {
            CosmosClient cosmosClient = new(Account, Key);
            service = new CosmosDbService(cosmosClient, DatabaseName, ContainerName);
            DatabaseResponse databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }

}
