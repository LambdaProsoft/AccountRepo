using Application.Interfaces.IStateAccount;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateAccountController : Controller
    {
        private readonly IStateAccountServices _stateAccountServices;

        public StateAccountController(IStateAccountServices stateAccountServices)
        {
            _stateAccountServices = stateAccountServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stateAccountServices.GetAllStateAccounts();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
