using Application.Request;
using Application.Response;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountServices
    {
        Task<AccountResponse> CreateAccount(AccountCreateRequest accountRequest);
        Task<AccountDetailsResponse> GetById(Guid id);
        Task<AccountResponse> UpdateAccount(Guid id, AccountUpdateRequest accountRequest);
        Task DisableAccount(Guid id);
        Task<bool> UpdateBalance(Guid id, AccountBalanceRequest balance);
    }
}