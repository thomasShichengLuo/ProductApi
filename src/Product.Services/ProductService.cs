using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Domain;
using Product.Data;
using Product.Services.Interfaces;

namespace Product.Services
{
    public class ProductService
        : IProductService
    {

        #region Constructor

            public ProductService(ProductDbContext dbContext)
            {
                _dbContext = dbContext;
            }

        #endregion


        #region Private Properties

            private readonly ProductDbContext _dbContext;


        #endregion

        
        #region Public Methods

            public async Task<Core.Domain.Product> GetProductById(Guid productId)
            {
                if (productId == default)
                {
                    throw new ArgumentNullException(nameof(productId));
                }

                var task = await _dbContext.Products
                    .FirstOrDefaultAsync(x => x.Id == productId);

                return task;
            }


            public async Task AddProduct(Core.Domain.Product product)
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product));
                }
                if(string.IsNullOrWhiteSpace(product.Name))
                {
                    throw new ArgumentNullException();
                }

                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
            }


            public async Task UpdateProduct(Core.Domain.Product product)
            {
                if (product == null)
                {
                    throw new ArgumentNullException();
                }
                var item = await GetProductById(product.Id);
                item.Name = product.Name;
                item.Status = product.Status;
                _dbContext.Products.Update(item);
                await _dbContext.SaveChangesAsync();
            }


            public async Task RemoveProduct(Guid productId)
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentNullException();
                }
                var product = await GetProductById(productId);

                product.IsDeleted = true;
                await _dbContext.SaveChangesAsync();

            }

            public async Task<IEnumerable<Core.Domain.Product>> GetProducts(int pageSize, int pageIndex)
            {
                if (pageSize <= 0)
                {
                    throw new ArgumentException();
                }
                if (pageIndex < 0)
                {
                    throw new ArgumentException();
                }
                return await _dbContext.Products.Where(x => x.IsDeleted == false).Skip(pageSize * pageIndex)
                    .Take(pageSize).ToListAsync();
        }

            public async Task<IEnumerable<Core.Domain.Product>> GetProductsByName(string name, int pageSize, int pageIndex)
            {
                if (pageSize <= 0)
                {
                    throw new ArgumentException();
                }
                if (pageIndex < 0)
                {
                    throw new ArgumentException();
                }
                return await _dbContext.Products.Where(x => x.IsDeleted == false && x.Name.Contains(name.Trim()))
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize).ToListAsync();
            }
        #endregion
    }
}
