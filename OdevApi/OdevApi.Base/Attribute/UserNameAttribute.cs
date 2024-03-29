﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OdevApi.Base;

public class UserNameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            if (value is null)
                return new ValidationResult("Invalid Username field.");

            if (Regex.IsMatch(value.ToString(), @"\s", RegexOptions.Compiled))
                return new ValidationResult("Username is not contain any space characters.");

            return ValidationResult.Success;
        }
        catch
        {
            return new ValidationResult("Invalid Username field.");
        }
    }
}
