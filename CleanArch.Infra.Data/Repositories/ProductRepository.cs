using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        async Task<Product> IProductRepository.CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        async Task<Product> IProductRepository.GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        //async Task<Product> IProductRepository.GetProductCategoryAsync(int id)
        //{
        //    return await _context.Products.Include(p => p.Category)
        //        .SingleOrDefaultAsync(p => p.Id == id);
        //}

        async Task<IEnumerable<Product>> IProductRepository.GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        async Task<Product> IProductRepository.RemoveAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        async Task<Product> IProductRepository.UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
