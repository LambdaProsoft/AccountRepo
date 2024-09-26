using Account.API.Infrastructure;
using Application.Interfaces.IAccountModel;
using Domain.Models;

namespace AccountInfrastructure.Query
{
    public class AccountQuery : IAccountQuery
    {
        private readonly AccountContext _context;

        public AccountQuery(AccountContext context)
        {
            _context = context;
        }
        public Task<AccountModel> GetAccount(Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}
