using OdevApi.Base;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace OdevApi.Web.Controllers;



[Route("odev/api/v1.0/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService service;
    public AccountController(IAccountService service)
    {
        this.service = service;
    }


    [HttpGet]
    [Authorize]
    public BaseResponse<List<AccountDto>> GetAll()
    {
        Log.Debug("AccountController.GetAll");
        var response = service.GetAll();
        return response;
    }

    [HttpGet("GetUserDetail")]
    [Authorize]
    public BaseResponse<AccountDto> GetByUsername()
    {
        Log.Debug("AccountController.GetByUsername");
        var id = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        var response = service.GetById(int.Parse(id));
        return response;
    }


    [HttpGet("{id}")]
    [Authorize]
    public BaseResponse<AccountDto> GetById([FromRoute] int id)
    {
        Log.Debug("AccountController.GetById");
        var response = service.GetById(id);
        return response;
    }

    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public BaseResponse<bool> Post([FromBody] AccountDto request)
    {
        Log.Debug("AccountController.Post");
        var response = service.Insert(request);
        return response;
    }

    [HttpPut("{id}")]
    [Authorize]
    public BaseResponse<bool> Put(int id, [FromBody] AccountDto request)
    {
        Log.Debug("AccountController.Put");
        var response = service.Update(id, request);
        return response;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public BaseResponse<bool> Delete(int id)
    {
        Log.Debug("AccountController.Delete");
        var response = service.Remove(id);
        return response;
    }

}
