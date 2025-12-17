using StartupProject.Dtos;
using System.Text.Json;

namespace StartupProject.Service.Interface
{
    public interface IQuizService
    {
        Task<List<QuizDTO>> GetAllAsync();
        Task<QuizDTO?> GetByIdAsync(Guid id);
        Task<QuizDTO> CreateAsync(QuizDTO dto);
        Task<bool> UpdateAsync(Guid id, QuizDTO dto);
        Task<bool> PatchAsync(Guid id, JsonElement patchData);
        Task<bool> DeleteAsync(Guid id);
    }
}
