using zad5.Models;

namespace zad5.dal
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Person>> GetPersonsAsync(string queryString);
        Task<Person?> GetPersonAsync(string id);
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(Person person);

        Task<IEnumerable<Job>> GetJobsAsync(string queryString);
        Task<Job?> GetJobAsync(string id);
        Task AddJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(Job job);

    }
}
