using Account.API.Infrastructure;
using Application.Interfaces.IAccountModel;
using Domain.Models;

namespace Infrastructure.Command
{
    public class AccountCommand : IAccountCommand
    {
        private readonly AccountContext _context;

        public AccountCommand(AccountContext context)
        {
            _context = context;
        }
        public async Task InsertAccount(AccountModel account)
        {
            await _context.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAccount(AccountModel account)
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
