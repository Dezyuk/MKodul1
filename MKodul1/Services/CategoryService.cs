using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKodul1.Context;
using MKodul1.Entity;
using MKodul1.Exceptions;
using MKodul1.Services.ServicesInterface;

namespace MKodul1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly QuizDBContext _context;

        public CategoryService(QuizDBContext quizDBContext)
        {
            _context = quizDBContext;
        }

        public async Task CreateCategory(string categoryTitle)
        {
            if (CategoryExists(categoryTitle))
            {
                throw new CategoryException(nameof(categoryTitle), "Такая категория уже существует.");
            }
            if (string.IsNullOrWhiteSpace(categoryTitle) || categoryTitle.Length < 3 || categoryTitle.Length > 50)
            {
                throw new CategoryValidationException(nameof(categoryTitle), "Название категории должно быть от 3 до 50 символов.");
            }
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Title = categoryTitle
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        private bool CategoryExists(string title)
        {
            return _context.Categories.FirstOrDefault(e => e.Title.ToLower() == title.ToLower()) is null ? false : true;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
