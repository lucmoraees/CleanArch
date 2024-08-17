using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<Category> ICategoryRepository.CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        async Task<IEnumerable<Category>> ICategoryRepository.GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        async Task<Category> ICategoryRepository.GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        async Task ICategoryRepository.RemoveAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        async Task<Category> ICategoryRepository.UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
