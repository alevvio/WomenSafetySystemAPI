using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WomenSafetySystemApi.Data;
using WomenSafetySystemApi.Models.Domain;
using WomenSafetySystemApi.Models.DTO;
using WomenSafetySystemApi.Repositories;

namespace WomenSafetySystemApi.Controllers;

//hostname.com/contacts/
[ApiController]
[Route("contacts")]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository contactRepository;
    public ContactsController(IContactRepository contactRepository)
    {
        this.contactRepository = contactRepository;
    }
    
    //Get all Contacts - hostname.com/contacts/
    [HttpGet]
    // [Authorize(Roles = "User")]   
    public async Task<IActionResult> GetAll()
    {
        //Get data from Domain Models
        var contacts = await contactRepository.GetAllAsync();

        //Map Domain Models to DTOs
        var contactDTO = new List<ContactDTO>();
        foreach (var contact in contacts)
        {
            contactDTO.Add(new ContactDTO()
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Address = contact.Address
            });
        }

        //Return DTOs
        return Ok(contactDTO);
    }

    //Create a new Contact - hostname.com/contacts/
    [HttpPost]
    // [Authorize(Roles = "User")]
    public async Task<IActionResult> Create([FromBody] AddUpdateContactDTO addUpdateContactDTO)
    {
        if (ModelState.IsValid)
        {
            //Map DTO to Domain Model
            var contactDomainModel = new ContactInfo
            {
                Name = addUpdateContactDTO.Name,
                Email = addUpdateContactDTO.Email,
                Phone = addUpdateContactDTO.Phone,
                Address = addUpdateContactDTO.Address
            };

            //Use Domain Model to Create Incident
            contactDomainModel = await contactRepository.CreateAsync(contactDomainModel);

            //Map Domain Model back to DTO to show User
            var contactDTO = new ContactDTO
            {
                Id = contactDomainModel.Id,
                Name = contactDomainModel.Name,
                Phone = contactDomainModel.Phone,
                Address = contactDomainModel.Address,
                Email = contactDomainModel.Email
            };

            //Return DTO to user
            return CreatedAtAction(nameof(Update), new { id = contactDTO.Id }, contactDTO);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    //Update a Contact - hostname.com/contacts/{id}
    [HttpPut]
    [Route("{id:guid}")]
    // [Authorize(Roles = "User")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddUpdateContactDTO addUpdateContactDTO)
    {
        if (ModelState.IsValid)
        {
            //Map DTO to Domain Model
            var contactDomainModel = new ContactInfo
            {
                Name = addUpdateContactDTO.Name,
                Email = addUpdateContactDTO.Email,
                Phone = addUpdateContactDTO.Phone,
                Address = addUpdateContactDTO.Address
            };

            //Check if Contact exists
            contactDomainModel = await contactRepository.UpdateAsync(id, contactDomainModel);

            //Return DTO to user
            if (contactDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model back to DTO to show User
            var contactDTO = new ContactDTO
            {
                Id = contactDomainModel.Id,
                Name = contactDomainModel.Name,
                Phone = contactDomainModel.Phone,
                Address = contactDomainModel.Address,
                Email = contactDomainModel.Email
            };

            return Ok(contactDTO);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    //Delete a Contact - hostname.com/contacts/{id}
    [HttpDelete]
    [Route("{id:guid}")]
    // [Authorize(Roles = "User")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        //Get Contact by Id from Domain Model
        var contactDomainModel = await contactRepository.DeleteAsync(id);
        
        //Return DTO to user
        if(contactDomainModel == null)
        {
            return NotFound();
        }

        return Ok("Contact deleted"); 
    }
}