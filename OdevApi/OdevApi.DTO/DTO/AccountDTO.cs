using OdevApi.Base;
using System.ComponentModel.DataAnnotations;

namespace OdevApi.Dto;

public class AccountDto : BaseDto
{
    [Required]
    [MaxLength(100)]
    [UserNameAttribute]
    [Display(Name = "UserName")]
    public string UserName { get; set; }

    [Required]
    [PasswordAttribute]
    public string Password { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    [Required]
    [EmailAddressAttribute]
    [MaxLength(250)]
    public string Email { get; set; }

    [Required]
    [RoleAttribute]
    public string Role { get; set; }


    [Display(Name = "Last Activity")]
    public DateTime LastActivity { get; set; }
}
