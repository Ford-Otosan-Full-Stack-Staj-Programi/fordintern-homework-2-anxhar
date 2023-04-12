using OdevApi.Base;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace OdevApi.Web;


[Route("odev/api/v1.0/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenManagementService tokenManagementService;
    public TokenController(ITokenManagementService tokenManagementService)
    {
        this.tokenManagementService = tokenManagementService;
    }


    [HttpPost]
    public BaseResponse<TokenResponse> Login([FromBody] TokenRequest request)
    {
        var response = tokenManagementService.GenerateToken(request);
        return response;
    }


}
