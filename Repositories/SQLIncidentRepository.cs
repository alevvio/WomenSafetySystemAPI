using Microsoft.EntityFrameworkCore;
using WomenSafetySystemApi.Data;
using WomenSafetySystemApi.Models.Domain;

namespace WomenSafetySystemApi.Repositories;

public class SQLIncidentRepository : IIncidentRepository
{
    private readonly WSSDbContext dbContext;
    public SQLIncidentRepository(WSSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    //POST Action Method Implementation
    public async Task<IncidentInfo> CreateAsync(IncidentInfo incidentInfo)
    {
        await dbContext.Incidents.AddAsync(incidentInfo);
        await dbContext.SaveChangesAsync();
        return incidentInfo;
    }

    //GET ByID Action Method Implementation
    public async Task<IncidentInfo?> GetByIdAsync(Guid id)
    {
        return await dbContext.Incidents.FirstOrDefaultAsync(x => x.Id == id);
    }
}