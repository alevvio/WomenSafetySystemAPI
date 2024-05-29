using System.ComponentModel.DataAnnotations;

namespace WomenSafetySystemApi.Models.DTO;

public class AddUpdateContactDTO
{
    [Required]
    [MaxLength(100, ErrorMessage = "Name can be a maximum of 100 characters")]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public string Address { get; set; }
}