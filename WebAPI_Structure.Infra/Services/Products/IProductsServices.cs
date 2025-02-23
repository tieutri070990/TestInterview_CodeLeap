using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_Structure.App.DTO;

namespace WebAPI_Structure.Infra.Services.Products
{
    public interface IProductsServices
    {
        Task<ErrorOr<List<ProductDTO>>> GetAllProduct();

        Task<ErrorOr<int>> CreateOrUpdate(ProductDTO req);


        Task<ErrorOr<int>> Delete(int Id);

        Task<ErrorOr<List<ProductDTO>>> SearchByName(string name);
    }
}
