﻿using System.ComponentModel.DataAnnotations;


namespace OdevApi.Base;

public abstract class BaseDto
{
    public int Id { get; set; }


    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }


    [MaxLength(250)]
    [Display(Name = "Created By")]
    public string CreatedBy { get; set; }
}
