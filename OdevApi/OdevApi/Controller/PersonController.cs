using OdevApi.Base;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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


    [HttpGet]
    [Authorize]
    [ResponseCache(CacheProfileName = "Duration45")]
    public BaseResponse<List<PersonDto>> GetAll()
    {
        Log.Debug("PersonController.GetAll");
        var response = service.GetAll();
        return response;
    }


    [HttpGet("{id}")]
    [Authorize]
    public BaseResponse<PersonDto> GetById(int id)
    {
        Log.Debug("PersonController.GetById");
        var response = service.GetById(id);
        return response;
    }

    [HttpPost]
    [Authorize]
    public BaseResponse<bool> Post([FromBody] PersonDto request)
    {
        Log.Debug("PersonController.Post");
        var response = service.Insert(request);
        return response;
    }

    [HttpPut("{id}")]
    [Authorize]
    public BaseResponse<bool> Put(int id, [FromBody] PersonDto request)
    {
        Log.Debug("PersonController.Put");
        request.Id = id;
        var response = service.Update(id, request);
        return response;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public BaseResponse<bool> Delete(int id)
    {
        Log.Debug("PersonController.Delete");
        var response = service.Remove(id);
        return response;
    }

}
