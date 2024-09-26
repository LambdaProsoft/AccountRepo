using Application.Interfaces.IAccountType;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeServices _accountTypeServices;

        public AccountTypeController(IAccountTypeServices accountTypeServices)
        {
            _accountTypeServices = accountTypeServices;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenericResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountTypeServices.GetAllAccountTypes();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
