using System.ComponentModel.DataAnnotations;

namespace WomenSafetySystemApi.Models.DTO;

public class LoginResponseDTO
{
    public string JwtToken { get; set; }
}