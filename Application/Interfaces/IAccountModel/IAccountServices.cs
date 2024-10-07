using Application.Request;
using Application.Response;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountServices
    {
        Task<AccountResponse> CreateAccount(AccountCreateRequest accountRequest);
        Task<AccountDetailsResponse> GetById(Guid id);
        Task<AccountDetailsResponse> GetByUserId(int userId);
        Task<AccountResponse> UpdateAccount(Guid id, AccountUpdateRequest accountRequest);
        Task<bool> UpdateBalance(Guid id, AccountBalanceRequest balance);
        Task<AccountResponse> DisableAccountByUser(int UserId);
    }
}