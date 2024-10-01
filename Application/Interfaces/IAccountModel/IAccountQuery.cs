using Domain.Models;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountQuery
    {
        Task<AccountModel> GetAccount(Guid accountId);
        Task<bool> IsAccountNumberUnique(string accountNumber);
        Task<bool> IsCbuUnique(string cbu);
        Task<bool> IsAliasUnique(string alias);
    }
}
