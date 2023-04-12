using OdevApi.Base;
using System.ComponentModel.DataAnnotations;

namespace OdevApi.Dto;

public class PersonDto : BaseDto
{
    [Required]
    [MaxLength(30)]
    [Display(Name = "Account Id")]
    public int AccountId { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [EmailAddress]
    [MaxLength(250)]
    public string Email { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Phone]
    [MaxLength(11)]
    public string Phone { get; set; }

    [Required]
    [DateOfBirth]
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; }


    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }

}
