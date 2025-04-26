using Microsoft.AspNetCore.Mvc;
using MKodul1.Entity;

namespace MKodul1.Services.ServicesInterface
{
    public interface ICategoryService
    {
        Task CreateCategory(string categoryTitle);
        Task<List<Category>> GetAllCategories();
    }
}
