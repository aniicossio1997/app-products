using app_products.Services.IServices;
using app_products.ViewModels.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace app_products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _service;

        public CategoriesController(ICategoriesService service)
        {
            _service = service;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CategoryFilterViewModel filters)
        {
            var result = await _service.GetByFilter(filters);
            return Ok(result);
        }
    }
}
