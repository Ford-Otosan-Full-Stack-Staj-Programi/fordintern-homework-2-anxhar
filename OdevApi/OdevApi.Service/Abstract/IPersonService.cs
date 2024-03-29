﻿using OdevApi.Base;
using OdevApi.Data;
using OdevApi.Dto;
using OdevApi.Service.Base;
using System;
namespace OdevApi.Service.Abstract;

public interface IPersonService : IBaseService<PersonDto, Person>
{
    BaseResponse<List<PersonDto>> GetAllByAccountId(int accountId);

}
