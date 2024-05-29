using System.ComponentModel.DataAnnotations;

namespace WomenSafetySystemApi.Models.DTO;

public class AddIncidentDTO
{
    [Required]
    public string Subject { get; set;}
    [Required]
    [MinLength(50, ErrorMessage = "The minimum length is 50 characters")]
    public string Details { get; set;}
    [Required]
    public bool Resolved { get; set; }
}