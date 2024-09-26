using Domain.Models;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountQuery
    {
        Task<AccountModel> GetAccount(Guid accountId);
    }
}
