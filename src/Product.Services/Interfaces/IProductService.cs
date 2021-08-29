using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Product.Core.Domain;

namespace Product.Services.Interfaces
{
    public interface IProductService
    {
        Task<Core.Domain.Product> GetProductById(Guid productId);

        Task AddProduct(Core.Domain.Product product);

        Task UpdateProduct(Core.Domain.Product product);

        Task RemoveProduct(Guid productId);

        Task<IEnumerable<Core.Domain.Product>> GetProductsByName(string name, int pageSize, int pageIndex);
        Task<IEnumerable<Core.Domain.Product>> GetProducts(int pageSize, int pageIndex);
    }
}