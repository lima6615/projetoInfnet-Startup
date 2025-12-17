using Startup.Domain.Entity;
using StartupProject.Data;
using Microsoft.EntityFrameworkCore;
using StartupProject.Repository.Interfaces;

namespace StartupProject.Repository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> FindAllAsync()
        {
            return await _context.Quiz
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<Quiz?> GetByIdAsync(Guid id)
        {
            return await _context.Quiz
                                 .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Quiz> CreateAsync(Quiz entity)
        {
            await _context.Quiz.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Quiz entity)
        {
            _context.Quiz.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(Quiz entity)
        {
            _context.Quiz.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Quiz.FirstOrDefaultAsync(q => q.Id == id);
            if (entity is null) return false;

            _context.Quiz.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}