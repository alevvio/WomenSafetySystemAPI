using Microsoft.AspNetCore.Identity;
using WomenSafetySystemApi.Models.Domain;

namespace WomenSafetySystemApi.Repositories;

public interface ITokenRepository 
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
}