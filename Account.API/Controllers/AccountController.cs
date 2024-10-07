using Application.Interfaces.IAccountModel;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Http.HttpResults;
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

        /// <summary>
        /// Obtiene una cuenta por su ID
        /// </summary>
        /// <param name="id">El ID de la cuenta</param>
        /// <returns>La información de la cuenta solicitada, junto con el usuario y transferencias</returns>
        [HttpGet]
        [ProducesResponseType(typeof(AccountDetailsResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            try
            {
                var accountResponse = await _accountServices.GetById(id);

                if (accountResponse == null)
                {
                    return NotFound();
                }

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        /// <summary>
        /// Actualiza una cuenta por su Id y datos ingresados
        /// </summary>
        /// <param name="accountId">El ID de la cuenta</param>
        /// <param name="accountRequest">Objeto de solicitud que contiene la información para actualizar una cuenta</param>
        /// <returns>La cuenta con los datos modificados</returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateAccount(Guid accountId, [FromBody] AccountUpdateRequest accountRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _accountServices.UpdateAccount(accountId, accountRequest);
                
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }

        }
        [HttpPut("Update/Balance")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateBalancee(Guid accountId, AccountBalanceRequest balance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = await _accountServices.UpdateBalance(accountId, balance);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Obtiene una cuenta por usuario
        /// </summary>
        /// <param name="userId">El ID de un usuario</param>
        /// <returns>La información de la cuenta solicitada, junto con el usuario y transferencias</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(AccountDetailsResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAccountByUserId(int userId)
        {
            try
            {
                var accountResponse = await _accountServices.GetByUserId(userId);

                if (accountResponse == null)
                {
                    return NotFound();
                }

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Deshabilita una cuenta por usuario
        /// </summary>
        /// <param name="userId">El ID de un usuario</param>
        /// <returns>Informacion de la cuenta con el estado actualizado</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(AccountResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccountByUser(int userId)
        {
            try
            {
                var accountResponse = await _accountServices.DisableAccountByUser(userId); ;

                if (accountResponse == null)
                {
                    return NotFound();
                }

                return Ok(accountResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}
