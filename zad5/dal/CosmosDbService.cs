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

        public async Task AddPersonAsync(Person person) => await container.CreateItemAsync(person, new PartitionKey(person.Id));

        public async Task UpdatePersonAsync(Person person) => await container.UpsertItemAsync(person, new PartitionKey(person.Id));

        public async Task DeletePersonAsync(Person person) => await container.DeleteItemAsync<Person>(person.Id, new PartitionKey(person.Id));

        public async Task<IEnumerable<Person>> GetPersonsAsync(string queryString)
        {
            List<Person> persons = new(); 
            var query = container.GetItemQueryIterator<Person>(new QueryDefinition(queryString));
            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();

                result.ToList().ForEach(r =>
                {
                    if (r.Type == nameof(Person) )
                    {
                        persons.Add(r);
                    }
                });

           
            }
            return persons;
        }

        public async Task<Person?> GetPersonAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Person>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return null;
            }
        }


        public async Task<IEnumerable<Job>> GetJobsAsync(string queryString)
        {
            List<Job> jobs = new();
            var query = container.GetItemQueryIterator<Job>(new QueryDefinition(queryString));
            while (query.HasMoreResults)
            {
                var result = await query.ReadNextAsync();

                result.ToList().ForEach(r =>
                {
                    if (r.Type == nameof(Job))
                    {
                        jobs.Add(r);
                    }
                });
            }
            return jobs;
        }

        public async Task<Job?> GetJobAsync(string id)
        {
            try
            {
                return await container.ReadItemAsync<Job>(id, new PartitionKey(id));
            }
            catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return null;
            }
        }
        public async Task AddJobAsync(Job job) => await container.CreateItemAsync(job, new PartitionKey(job.Id));

        public async Task UpdateJobAsync(Job job) => await container.UpsertItemAsync(job, new PartitionKey(job.Id));

        public async Task DeleteJobAsync(Job job) => await container.DeleteItemAsync<Job>(job.Id, new PartitionKey(job.Id));

      
    }
}
