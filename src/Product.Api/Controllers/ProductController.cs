using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Core.Domain;
using Product.Services.Interfaces;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController 
        : ControllerBase
    {
        #region Constructor

            public ProductController(IProductService productService, ILogger<ProductController> logger, IHelper helper)
            {
                _productService = productService;
                _logger = logger;
                _helper = helper;
            }

        #endregion


        #region Private Properties

            private readonly IProductService _productService;
            private readonly ILogger _logger;
            private readonly IHelper _helper;

        #endregion


        #region Public Methods

        [HttpGet("{id}")]
            public async Task<ActionResult<Core.Domain.Product>> Get(Guid id, string readKey)
            {
                _logger.LogInformation("GetProduct: " + id);
                if (!_helper.ReadAuthorization( readKey))
                {
                    _logger.LogInformation("Invalid read Key");
                    return StatusCode(403);
                }
                try
                {
                    return Ok(await _productService.GetProductById(id));
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    return BadRequest();
                }
            
            }
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Core.Domain.Product>>> Get(int pageSize, int pageIndex, string readKey)
            {
                _logger.Log(LogLevel.Information, "Get All Products");
                if (!_helper.ReadAuthorization(readKey))
                {
                    _logger.LogInformation("Invalid read Key");
                    return StatusCode(403);
                }
                    
                
                try
                {
                    return Ok(await _productService.GetProducts(pageSize,  pageIndex));
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    return BadRequest();
                }
   
            }
            [HttpGet("search/{name}/pageSize/{pageSize}/pageIndex/{pageIndex}")]
            public async Task<ActionResult<IEnumerable<Core.Domain.Product>>> GetByName(string name, int pageSize, int pageIndex, string readKey)
            {
                _logger.LogInformation("SearchProducts: " + name);
                if (!_helper.ReadAuthorization(readKey))
                {
                    _logger.LogInformation("Invalid read Key");
                    return StatusCode(403);
                }
                    
                if (pageSize <= 0)
                {
                    _logger.LogInformation("BadRequest  pageSize: ", pageSize);
                    return BadRequest();
                }
                if (pageIndex < 0)
                {
                    _logger.LogInformation("BadRequest pageIndex: ", pageIndex);
                    return BadRequest();
                }
                if(name != null)
                    name = _helper.FilterString(name.Trim());

                try
                {
                    return Ok(await _productService.GetProductsByName(name, pageSize, pageIndex));
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    return BadRequest();
                }
           
            }

            [HttpPost]
            public async Task<ActionResult> Post([FromBody] Core.Domain.Product product, string writeKey)
            {
                if (!_helper.WriteAuthorization(writeKey))
                {
                    _logger.LogInformation("Invalid write Key");
                    return StatusCode(403);
                }

                if (product == null)
                {
                    _logger.LogInformation("BadRequest Post product");
                    return BadRequest();
                }
                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    _logger.LogInformation("BadRequest", product);
                    return BadRequest();
                }


                if (product.Name != null)
                    product.Name = _helper.FilterString(product.Name.Trim());
                if (product.Status != null)
                    product.Status = _helper.FilterString(product.Status.Trim());
                try
                {
                    await _productService.AddProduct(product);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    return BadRequest();
                }
            }

        


            [HttpPut("{id}")]
            public async Task<ActionResult> Put(string id, [FromBody] Core.Domain.Product product, string writeKey)
            {
                if (!_helper.WriteAuthorization(writeKey))
                {
                    _logger.LogInformation("Invalid write Key");
                    return StatusCode(403);
                }
                    
                if (id == Guid.Empty.ToString())
                {
                    _logger.LogInformation("BadRequest", id);
                    return BadRequest();
                }
                if (product == null)
                {
                    _logger.LogInformation("BadRequest Post product");
                    return BadRequest();
                }
                if (product.Name != null)
                    product.Name = _helper.FilterString(product.Name.Trim());
                if (product.Status != null)
                    product.Status = _helper.FilterString(product.Status.Trim());
                if (String.IsNullOrEmpty(product.Name))
                {
                    _logger.LogInformation("BadRequest", product);
                    return BadRequest();
                }
                try
                {
                    await _productService.UpdateProduct(product);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    return BadRequest();
                }

            }


            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id, string writeKey)
            {
                if (!_helper.WriteAuthorization(writeKey))
                {
                    _logger.LogInformation("Invalid write Key");
                    return StatusCode(403);
                }
                if (id == Guid.Empty)
                {
                    _logger.LogInformation(new ArgumentNullException(), "Get({Id}) NOT FOUND", id);
                    return BadRequest();
                }
                
                try
                {
                    await _productService.RemoveProduct(id);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    return BadRequest();
                }
            }

        #endregion
    }
}
