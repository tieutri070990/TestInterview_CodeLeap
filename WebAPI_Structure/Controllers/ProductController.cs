using Microsoft.AspNetCore.Authorization; // added  Authorize
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Structure.App.DTO;
using WebAPI_Structure.Infra.Services.Products;

namespace WebAPI_Structure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsServices _productServices;
        public ProductController(IProductsServices productServices)
        {
            _productServices = productServices;
        }

        [Authorize]
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var data = await _productServices.GetAllProduct();
            if (data.IsError)
                return Ok(data.FirstError);

            return Ok(data.Value);
        }

        [Authorize]
        [HttpPost("SaveProduct")]
        public async Task<IActionResult> CreateOrUpdateProduct(ProductDTO req)
        {
            var data = await _productServices.CreateOrUpdate(req);
            if (data.IsError)
                return Ok(data.FirstError);

            return Ok(data.Value);
        }

        [Authorize]
        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var data = await _productServices.Delete(Id);
            if (data.IsError)
                return Ok(data.FirstError);

            return Ok(data.Value);
        }

        [Authorize]
        [HttpPost("SearchProductByName")]
        public async Task<IActionResult> SearchProductByName(string name)
        {
            var data = await _productServices.SearchByName(name);
            if (data.IsError)
                return Ok(data.FirstError);

            return Ok(data.Value);
        }
    }
}
