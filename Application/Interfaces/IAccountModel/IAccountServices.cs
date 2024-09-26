using Application.Request;
using Application.Response;

namespace Application.Interfaces.IAccountModel
{
    public interface IAccountServices
    {
        Task<AccountResponse> CreateAccount(AccountCreateRequest accountRequest);
        Task<AccountResponse> GetById(Guid id);
        Task<AccountResponse> UpdateAccount(AccountRequest accountRequest);
        Task DisableAccount(Guid id);
    }
}