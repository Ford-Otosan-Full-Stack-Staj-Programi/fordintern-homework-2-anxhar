using AutoMapper;
using OdevApi.Data;
using OdevApi.Dto;
using System.Security.Principal;
using System;

namespace OdevApi.Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();
        CreateMap<Account, PersonDto>().ForMember(x => x.AccountId, opt => opt.MapFrom(s => s.Id));

        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, Account>();


    }
}
