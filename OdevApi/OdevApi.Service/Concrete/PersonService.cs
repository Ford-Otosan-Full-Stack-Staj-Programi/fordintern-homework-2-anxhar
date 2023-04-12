using AutoMapper;
using OdevApi.Base;
using OdevApi.Data;
using OdevApi.Dto;
using OdevApi.Service.Abstract;
using OdevApi.Service.Base;

namespace OdevApi.Service.Concrete;

public class PersonService : BaseService<PersonDto, Person>, IPersonService
{
    private readonly IAccountService accountService;
    public PersonService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService, IGenericRepository<Person> genericRepository) : base(unitOfWork, mapper, genericRepository)
    {
        this.accountService = accountService;
    }

    public override BaseResponse<bool> Insert(PersonDto insertResource)
    {
        if (insertResource.DateOfBirth < DateTime.UtcNow.AddYears(18))
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