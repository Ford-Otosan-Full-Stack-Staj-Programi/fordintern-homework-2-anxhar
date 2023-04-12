using OdevApi.Base;
using OdevApi.Dto;

namespace OdevApi.Service.Abstract;

public interface ITokenManagementService
{
    BaseResponse<TokenResponse> GenerateToken(TokenRequest request);
}
