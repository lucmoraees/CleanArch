﻿using CleanArch.Application.CQRS.Products.Commands;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using MediatR;

namespace CleanArch.Application.CQRS.Products.Handlers
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new
                ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductRemoveCommand request,
        CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                throw new ApplicationException($"Entity could not be found.");
            }
            else
            {
                var result = await _productRepository.RemoveAsync(product);
                return result;
            }
        }
    }
}
