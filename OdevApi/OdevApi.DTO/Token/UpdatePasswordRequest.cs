using OdevApi.Base;
using System.ComponentModel.DataAnnotations;

namespace OdevApi.Dto;

public class UpdatePasswordRequest
{
    [Required]
    [PasswordAttribute]
    public string OldPassword { get; set; }

    [Required]
    [Password]
    public string NewPassword { get; set; }
}
