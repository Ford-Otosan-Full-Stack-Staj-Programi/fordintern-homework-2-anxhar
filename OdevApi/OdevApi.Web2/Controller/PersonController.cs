using OdevApi.Base;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using OdevApi.Data;
using System.Security.Claims;
using Azure;

namespace OdevApi.Web.Controllers;

[Route("odev/api/v1.0/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService service;
    public PersonController(IPersonService service)
    {
        this.service = service;
    }


    [HttpGet("GetAll")]
    [Authorize]

    public BaseResponse<List<PersonDto>> GetAllByAccountId()
    {
        Log.Debug("PersonController.GetAll");
        var id = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AccountId").Value);
        var response = service.GetAllByAccountId(id);
        return response;
    }

    [HttpPost]
    [Authorize]
    public BaseResponse<bool> Post([FromBody] PersonDto request)
    {
        Log.Debug("PersonController.Post");
        var claimsList = User.Claims;
        var id = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        request.AccountId = Int32.Parse(id);
        var response = service.Insert(request);
        return response;
    }

    [HttpPut("{id}")]
    [Authorize]
    public BaseResponse<bool> Put(int id, [FromBody] PersonDto request)
    {
        Log.Debug("PersonController.Put");
        request.Id = id;
        return service.Update(id, request); ;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public BaseResponse<bool> Delete(int id, string accountId)
    {
        Log.Debug("PersonController.Delete");
        string otoAccountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
        BaseResponse<bool> response = null;
        if (accountId == otoAccountId)
        {
            response = service.Remove(id);           
        }
        return response;
    }

}
