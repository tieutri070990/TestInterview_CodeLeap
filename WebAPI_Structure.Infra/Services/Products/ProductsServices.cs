using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Structure.App.DTO;
using WebAPI_Structure.Core.Models;
using WebAPI_Structure.Infra.Context;

namespace WebAPI_Structure.Infra.Services.Products
{
    public class ProductsServices : IProductsServices   
    {
        private readonly DemoTestDBConText _context;
        public ProductsServices(DemoTestDBConText context)
        {
            _context = context;
        }
       public async Task<ErrorOr<List<ProductDTO>>> GetAllProduct()
        {
            return await _context.Products
                .Select(x => new ProductDTO 
                { 
                    ProductId = x.Id, 
                    ProductName = x.Name,
                    Description = x.Description,
                    ProductPrice = x.Price,
                    Notes = x.Notes,
                    ProductImageUrl = x.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<ErrorOr<int>> CreateOrUpdate(ProductDTO req)
        {
            if (req.ProductId == 0)
            {
                var product = new Product
                {
                    Name = req.ProductName,
                    Description = req.Description,
                    Price = req.ProductPrice,
                    ImageUrl = req.ProductImageUrl,
                    Notes = req.Notes
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
            else
            {
                var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == req.ProductId);
                if (product == null)
                {
                    return Error.Failure("Product not found");
                }

                product.Name = req.ProductName;
                product.Description = req.Description;
                product.Price = req.ProductPrice;
                product.ImageUrl = req.ProductImageUrl;
                product.Notes = req.Notes;

                await _context.SaveChangesAsync();
                return product.Id;
            }
          
        }

        public async Task<ErrorOr<int>> Delete(int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == Id);
            if(product == null)
            {
                return Error.Failure("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Id;
        }

        public async Task<ErrorOr<List<ProductDTO>>> SearchByName(string name)
        {
            var listProduct = await _context.Products
                .Where(x => x.Name.Contains(name))
                .Select(x => new ProductDTO
                {
                    ProductId = x.Id,
                    ProductName = x.Name,
                    Description = x.Description,
                    ProductPrice = x.Price,
                    Notes = x.Notes,
                    ProductImageUrl = x.ImageUrl
                })
                .ToListAsync();

            if(listProduct.Count == 0)
            {
                return Error.Failure("Product not found");
            }

            return listProduct;
        }

        
    }
}
