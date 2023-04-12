using OdevApi.Base;
using OdevApi.Data;
using OdevApi.Dto;
using OdevApi.Service.Base;
using System.Security.Principal;
namespace OdevApi.Service.Abstract;

public interface IAccountService : IBaseService<AccountDto, Account>
{
    BaseResponse<AccountDto> GetByUsername(string username);
}
