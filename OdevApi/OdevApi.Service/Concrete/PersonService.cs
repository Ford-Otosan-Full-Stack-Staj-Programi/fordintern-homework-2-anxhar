using AutoMapper;
using OdevApi.Base;
using OdevApi.Data;
using OdevApi.Data.Repo.Abstract;
using OdevApi.Data.Repo.Concrete;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Service.Base;
using System.Collections;
using System.Collections.Generic;

namespace OdevApi.Service.Concrete;

public class PersonService : BaseService<PersonDto, Person>, IPersonService
{
    private readonly IAccountService accountService;
    private readonly IPersonRepo personRepo;
    private readonly IMapper mapper;
    public PersonService(IUnitOfWork unitOfWork,IPersonRepo personRepo, IMapper mapper, IAccountService accountService, IGenericRepository<Person> genericRepository) : base(unitOfWork, mapper, genericRepository)
    {
        this.accountService = accountService;
        this.personRepo = personRepo;
        this.mapper = mapper;
    }

    public BaseResponse<List<PersonDto>> GetAllByAccountId(int accountId)
    {
        var persons = personRepo.GetAllByAccountId(accountId);
        var personDtos = mapper.Map<List<PersonDto>>(persons);
        return new BaseResponse<List<PersonDto>>(personDtos);
    }

    public override BaseResponse<bool> Insert(PersonDto insertResource)
    {
        if (insertResource.DateOfBirth > DateTime.UtcNow.AddYears(50))
        {
            return new BaseResponse<bool>("DateOfBirth was incorrect");
        }

        var response = accountService.GetByUsername(insertResource.Email);
        if (!response.Success)
        {
            return new BaseResponse<bool>(response.Message);
        }

        AccountDto account = response.Response;

        return base.Insert(insertResource);
    }

}