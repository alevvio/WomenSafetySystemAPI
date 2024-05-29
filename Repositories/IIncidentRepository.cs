using WomenSafetySystemApi.Models.Domain;

namespace WomenSafetySystemApi.Repositories;

public interface IIncidentRepository
{
    //POST Action Method
    Task<IncidentInfo> CreateAsync(IncidentInfo incidentInfo);
    //GET ByID Action Method
    Task<IncidentInfo?> GetByIdAsync(Guid Id);
}