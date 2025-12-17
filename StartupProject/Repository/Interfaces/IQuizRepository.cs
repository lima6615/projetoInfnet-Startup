using Startup.Domain.Entity;

namespace StartupProject.Repository.Interfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> FindAllAsync();
        Task<Quiz?> GetByIdAsync(Guid id);
        Task<Quiz> CreateAsync(Quiz entity);
        Task<bool> UpdateAsync(Quiz entity);
        Task<bool> PatchAsync(Quiz entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
