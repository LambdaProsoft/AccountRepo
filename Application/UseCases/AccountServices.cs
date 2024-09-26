using Application.Interfaces.IAccountModel;
using Application.Interfaces.IAccountType;
using Application.Interfaces.IStateAccount;
using Application.Interfaces.ITypeCurrency;
using Application.Request;
using Application.Response;
using Domain.Models;

namespace Application.UseCases
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountCommand _accountCommand;
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly ITypeCurrencyServices _typeCurrencyServices;
        private readonly IStateAccountServices _stateAccountServices;

        public AccountServices(IAccountCommand accountCommand,
            IAccountTypeServices accountTypeServices,
            ITypeCurrencyServices typeCurrencyServices,
            IStateAccountServices stateAccountServices)
        {
            _accountCommand = accountCommand;
            _accountTypeServices = accountTypeServices;
            _typeCurrencyServices = typeCurrencyServices;
            _stateAccountServices = stateAccountServices;
        }

        public async Task<AccountResponse> CreateAccount(AccountCreateRequest accountRequest)
        {
            string accountNumber = GenerateAccountNumber();

            string cbu = GenerateCBU();

            string alias = GenerateAlias();

            var account = new AccountModel
            {
                AccountId = Guid.NewGuid(),
                CBU = cbu,
                Alias = alias,
                NumberAccount = accountNumber,
                Balance = 0,  // Arranca con plata en 0 a menos definamos promos (sugerencia lucas)
                UserId = accountRequest.User,
                AccTypeId = accountRequest.AccountType,
                CurrencyId = accountRequest.Currency,
                StateId = 1 //Por defecto la iniciamos activa
            };

            await _accountCommand.InsertAccount(account);

            var response = new AccountResponse
            {
                CBU = account.CBU,
                Alias = account.Alias,
                NumeroDeCuenta = account.NumberAccount,
                Balance = account.Balance,
                TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
            };

            return response;
        }

        public string GenerateAccountNumber()
        {
            // Suponiendo que 4748 es el código de nuestra wallet.
            string bankCode = "4748";
            string sequentialNumber = new Random().Next(10000000, 99999999).ToString();
            return $"{bankCode}-{sequentialNumber}";
        }

        public string GenerateCBU()
        {
            // Suponiendo que 28505909 es el código bancario para transferencias de nuestra wallet.
            string bankCode = "28505909";
            var random = new Random();
            string randomSequence = random.Next(100000000, 999999999).ToString() + random.Next(10000, 99999).ToString();

            return $"{bankCode}{randomSequence}";
        }

        public string GenerateAlias()
        {
            var words = new List<string>
            {
                "águila", "león", "tigre", "luna", "río", "nube", "piedra", "bosque", "montaña", "lobo",
                "sol", "estrella", "océano", "sueño", "fuego", "tormenta", "árbol", "cielo", "viento", "sombra",
                "mar", "trueno", "nieve", "rayo", "flor", "campo", "jardín", "ciudad", "desierto", "isla"
            };

            var random = new Random();

            string word1 = words[random.Next(words.Count)];
            string word2 = words[random.Next(words.Count)];
            string word3 = words[random.Next(words.Count)];
             
            //podria agregarse una validacion para asegurar el alias es unico

            return $"{word1}.{word2}.{word3}";
        }

        public Task DisableAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> UpdateAccount(AccountRequest accountRequest)
        {
            throw new NotImplementedException();
        }
    }
}
