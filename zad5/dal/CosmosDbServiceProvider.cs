using Microsoft.Azure.Cosmos;

namespace zad5.dal
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "Persons";
        private const string ContainerName = "Tasks";
        private const string Account = "https://pppkpersonab.documents.azure.com:443/";
        private const string Key = "EyP7lbyIRj9w6Kh23LRdwfz3PUkNuwMHR2X3l4iqdjsvPlJJMnLRF45u3n1oF73LF9oy8j9RXuRJACDb5JbeMA==";

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
