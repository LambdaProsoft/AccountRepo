using Application.Exceptions;
using Application.Interfaces.IAccountModel;
using Application.Interfaces.IAccountType;
using Application.Interfaces.IHttpServices;
using Application.Interfaces.IStateAccount;
using Application.Interfaces.ITypeCurrency;
using Application.Request;
using Application.Response;
using Domain.Models;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace Application.UseCases
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountCommand _accountCommand;
        private readonly IAccountQuery _accountQuery;
        private readonly IAccountTypeServices _accountTypeServices;
        private readonly ITypeCurrencyServices _typeCurrencyServices;
        private readonly IStateAccountServices _stateAccountServices;

        private readonly IUserHttpService _userHttpService;
        private readonly ITransferHttpService _transferHttpService;

        private readonly Random _random;
        public AccountServices(IAccountCommand accountCommand,
            IAccountQuery accountQuery,
            IAccountTypeServices accountTypeServices,
            ITypeCurrencyServices typeCurrencyServices,
            IStateAccountServices stateAccountServices,
            ITransferHttpService transferHttpService,
            IUserHttpService userHttpService)
        {
            _accountCommand = accountCommand;
            _accountQuery = accountQuery;
            _accountTypeServices = accountTypeServices;
            _typeCurrencyServices = typeCurrencyServices;
            _stateAccountServices = stateAccountServices;
            _transferHttpService = transferHttpService;
            _userHttpService = userHttpService;

            _random = new Random();
        }

        public async Task<AccountResponse> CreateAccount(AccountCreateRequest accountRequest)
        {
            string accountNumber = await GenerateAccountNumber();

            string cbu = await GenerateCBU();

            string alias = await GenerateAlias();

            // Deberiamos validar que el userId no se repita en otras cuentas?

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

        public async Task<string> GenerateAccountNumber()
        {
            const int max = 20;
            int attempts = 0;

            string bankCode = "4748";
            string accountNumber;

            do
            {
                string sequentialNumber = _random.Next(10000000, 99999999).ToString();
                accountNumber = $"{bankCode}-{sequentialNumber}";
                attempts++;

                if (attempts == max)
                {
                    throw new InvalidOperationException("Account number could not be generated");
                }

                // Verifica que el numero de cuenta sea unico.
            } while (!await _accountQuery.IsAccountNumberUnique(accountNumber));

            return accountNumber;
        }

        public async Task<string> GenerateCBU()
        {
            const int max = 20;
            int attempts = 0;

            string bankCode = "28505909";
            string cbu;

            do
            {
                string randomSequence = _random.Next(100000000, 999999999).ToString() + _random.Next(10000, 99999).ToString();
                cbu = $"{bankCode}{randomSequence}";
                attempts++;

                if (attempts == max)
                {
                    throw new InvalidOperationException("CBU could not be generated");
                }

                // Verifica que el CBU sea unico.
            } while (!await _accountQuery.IsCbuUnique(cbu));

            return cbu;
        }

        public async Task<string> GenerateAlias()
        {
            const int max = 20;
            int attempts = 0;
            string alias;

            var words = new List<string>
            {
                "águila", "león", "tigre", "luna", "río", "nube", "piedra", "bosque", "montaña", "lobo",
                "sol", "estrella", "océano", "sueño", "fuego", "tormenta", "árbol", "cielo", "viento", "sombra",
                "mar", "trueno", "nieve", "rayo", "flor", "campo", "jardín", "ciudad", "desierto", "isla"
            };

            do
            {
                string word1 = words[_random.Next(words.Count)];
                string word2 = words[_random.Next(words.Count)];
                string word3 = words[_random.Next(words.Count)];

                alias = $"{word1}.{word2}.{word3}";
                attempts++;

                if (attempts == max)
                {
                    throw new InvalidOperationException("Alias could not be generated");
                }

                //Verifica que el alias sea único.
            } while (!await _accountQuery.IsAliasUnique(alias));

            return alias;
        }

        public async Task<AccountDetailsResponse> GetById(Guid id)
        {
            var account = await _accountQuery.GetAccountById(id);

            // Verificar si funciona con los MS completos
            var user = await _userHttpService.GetUserById(account.UserId)
                ?? throw new InvalidOperationException("Users not found");

            var transfers = await _transferHttpService.GetAllTransfersByAccount(account.AccountId)
                ?? throw new InvalidOperationException("Transfers not found");

            var response = new AccountDetailsResponse
            {
                Account = new AccountResponse
                {
                    CBU = account.CBU,
                    Alias = account.Alias,
                    NumeroDeCuenta = account.NumberAccount,
                    Balance = account.Balance,
                    TipoDeCuenta = _accountTypeServices.GetById(account.AccTypeId).Result.Name,
                    TipoDeMoneda = _typeCurrencyServices.GetById(account.CurrencyId).Result.Name,
                    EstadoDeLaCuenta = _stateAccountServices.GetById(account.StateId).Result.Name
                },

                // Suponiendo que los responses sean iguales
                User = user,
                Transfers = transfers
            };

            return response;
        }

        public async Task<AccountResponse> UpdateAccount(Guid id, AccountUpdateRequest accountRequest)
        {
            var account = await _accountQuery.GetAccountById(id);
            if (account == null)
            {
                return null;
            }

            account.Alias = accountRequest.Alias;
            account.CurrencyId = accountRequest.Currency;
            account.StateId = accountRequest.State;
            account.AccTypeId = accountRequest.AccountType;

            await _accountCommand.UpdateAccount(account);

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


        public Task DisableAccount(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBalance(Guid id, AccountBalanceRequest balance)
        {
            var account = await _accountQuery.GetAccountById(id);
            if (account == null)
            {
                return false;
            }
            else
            {
                await _accountCommand.UpdateBalance(id, balance.Balance);
                return true;
            }

        }
    }
}
