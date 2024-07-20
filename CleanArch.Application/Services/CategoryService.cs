using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        private IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Add(CategoryDTO CategoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(CategoryDto);
            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task Remove(int id)
        {
            var categoryEntity = _categoryRepository.GetCategoryByIdAsync(id).Result;
            await _categoryRepository.RemoveAsync(categoryEntity);
        }

        public async Task Update(CategoryDTO CategoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(CategoryDto);
            await _categoryRepository.UpdateAsync(categoryEntity);
        }
    }
}
