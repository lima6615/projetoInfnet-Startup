using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Startup.Domain.Entity;
using StartupProject.Dtos;
using StartupProject.Service.Interface;
using StartupProject.Repository;
using StartupProject.Repository.Interfaces;

namespace StartupProject.Service
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _repository;

        public QuizService(IQuizRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<QuizDTO>> GetAllAsync()
        {
            var quizzes = await _repository.FindAllAsync();
            return quizzes.Select(MapToDto).ToList();
        }

        public async Task<QuizDTO?> GetByIdAsync(Guid id)
        {
            var quiz = await _repository.GetByIdAsync(id);
            if (quiz is null) return null;
            return MapToDto(quiz);
        }

        public async Task<QuizDTO> CreateAsync(QuizDTO dto)
        {
            var entity = new Quiz
            {
                title = dto.title,
                Description = dto.Description,
                InAtivo = dto.InAtivo,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt
            };

            var created = await _repository.CreateAsync(entity);
            return MapToDto(created);
        }

        public async Task<bool> UpdateAsync(Guid id, QuizDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;

            entity.title = dto.title;
            entity.Description = dto.Description;
            entity.InAtivo = dto.InAtivo;
            entity.CreatedAt = dto.CreatedAt;

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> PatchAsync(Guid id, JsonElement patchData)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) return false;
            if (patchData.ValueKind != JsonValueKind.Object) return false;

            var handlers = new Dictionary<string, Action<JsonElement>>(StringComparer.OrdinalIgnoreCase)
            {
                ["title"] = v => { if (v.ValueKind == JsonValueKind.String) entity.title = v.GetString() ?? entity.title; },
                ["description"] = v => { if (v.ValueKind == JsonValueKind.String) entity.Description = v.GetString() ?? entity.Description; },
                ["inativo"] = v => { if (v.ValueKind == JsonValueKind.True || v.ValueKind == JsonValueKind.False) entity.InAtivo = v.GetBoolean(); },
                ["createdat"] = v =>
                {
                    if (v.ValueKind == JsonValueKind.String && DateTime.TryParse(v.GetString(), out var dt)) entity.CreatedAt = dt;
                    else if (v.ValueKind == JsonValueKind.Number && v.TryGetInt64(out var ticks)) entity.CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(ticks).UtcDateTime;
                }
            };

            foreach (var prop in patchData.EnumerateObject())
            {
                var key = prop.Name.Trim();
                if (handlers.TryGetValue(key, out var action)) action(prop.Value);
            }

            return await _repository.PatchAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static QuizDTO MapToDto(Quiz q) =>
            new QuizDTO
            {
                Id = q.Id,
                title = q.title,
                Description = q.Description,
                InAtivo = q.InAtivo,
                CreatedAt = q.CreatedAt
            };
    }
}