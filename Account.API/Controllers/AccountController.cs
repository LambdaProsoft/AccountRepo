using Application.Interfaces.IAccountModel;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        /// <summary>
        /// Crea una nueva cuenta bancaria
        /// </summary>
        /// <param name="accountRequest">Objeto de solicitud que contiene la información necesaria para crear una cuenta</param>
        /// <returns>El objeto de respuesta con los detalles de la cuenta creada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccountResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateRequest accountRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var accountResponse = await _accountServices.CreateAccount(accountRequest);

                return Created(string.Empty,accountResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        ///// <summary>
        ///// Obtiene una cuenta por su ID
        ///// </summary>
        ///// <param name="id">El ID de la cuenta</param>
        ///// <returns>La información de la cuenta solicitada</returns>
        //[HttpGet("{id:guid}")]
        //[ProducesResponseType(typeof(AccountResponse), 200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetAccountById(Guid id)
        //{
        //    try
        //    {
        //        var accountResponse = await _accountServices.GetById(id);

        //        if (accountResponse == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(accountResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = ex.Message });
        //    }
        //}
    }
}
