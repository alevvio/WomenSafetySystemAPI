using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WomenSafetySystemApi.Data;
using WomenSafetySystemApi.Models.Domain;
using WomenSafetySystemApi.Models.DTO;
using WomenSafetySystemApi.Repositories;

namespace WomenSafetySystemApi.Controllers;

//hostname.com/ireport/
[ApiController]
[Route("ireport")]
// [Authorize(Roles = "User")] 
public class IncidentReportController : ControllerBase
{
    private readonly IIncidentRepository incidentRepository;
    public IncidentReportController(IIncidentRepository incidentRepository)
    {
        this.incidentRepository = incidentRepository;
    }
    
    //Create a new Report - hostname.com/ireport/
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddIncidentDTO addIncidentDTO)
    {
        if (ModelState.IsValid)
        {
            //Map DTO to Domain Model
            var incidentDomainModel = new IncidentInfo
            {
                Subject = addIncidentDTO.Subject,
                Details = addIncidentDTO.Details,
                Resolved = addIncidentDTO.Resolved
            };

            //Use Domain Model to Create Incident
            incidentDomainModel = await incidentRepository.CreateAsync(incidentDomainModel);

            //Map Domain Model back to DTO to show User
            var incidentDTO = new IncidentDTO
            {
                Id = incidentDomainModel.Id,
                Subject = incidentDomainModel.Subject,
                Details = incidentDomainModel.Details,
                Resolved = incidentDomainModel.Resolved
            };

            //Return DTO to user
            return CreatedAtAction(nameof(GetById), new { id = incidentDTO.Id }, incidentDTO);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    //Get a Report by Id - hostname.com/ireport/{id}
    [HttpGet]
    [Route("{id:guid}")]
    // [Authorize(Roles = "User")] 
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        //Get Report by Id from Domain Model
        var existingIncident = await incidentRepository.GetByIdAsync(id);
        
        //Return DTO to User
        if(existingIncident == null){
            return NotFound("Not Found such Incident");
        }

        //Map Domain Model to DTO
        var incidentDTO = new IncidentDTO
        {
            Id = existingIncident.Id,
            Subject = existingIncident.Subject,
            Details = existingIncident.Details,
            Resolved = existingIncident.Resolved
        };

        return Ok(incidentDTO);
    }
}