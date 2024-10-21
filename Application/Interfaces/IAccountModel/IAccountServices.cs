using Application.Request;
using Application.Response;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountServices
    {
        Task<AccountResponse> CreateAccount(AccountCreateRequest accountRequest);
        Task<AccountDetailsResponse> GetById(Guid id);
        Task<AccountResponse> GetByUserId(int userId);
        Task<AccountResponse> UpdateAccount(Guid id, AccountUpdateRequest accountRequest);
        Task<TransferProcess> UpdateBalance(Guid id, AccountBalanceRequest balance);
        Task<AccountResponse> DisableAccountByUser(int UserId);
    }
}