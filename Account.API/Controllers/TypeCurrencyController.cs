using Application.Interfaces.ITypeCurrency;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeCurrencyController : Controller
    {
        private readonly ITypeCurrencyServices _typeCurrencyServices;

        public TypeCurrencyController(ITypeCurrencyServices typeCurrencyServices)
        {
            _typeCurrencyServices = typeCurrencyServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _typeCurrencyServices.GetAllTypeCurrencies();
            return new JsonResult(result) { StatusCode = 200 };
        }

    }
}
