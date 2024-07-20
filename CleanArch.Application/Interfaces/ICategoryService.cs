using CleanArch.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetById(int id);
        Task Add(CategoryDTO CategoryDto);
        Task Update(CategoryDTO CategoryDto);
        Task Remove(int id);
    }
}
