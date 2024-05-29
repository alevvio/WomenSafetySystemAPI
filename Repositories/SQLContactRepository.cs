using Microsoft.EntityFrameworkCore;
using WomenSafetySystemApi.Data;
using WomenSafetySystemApi.Models.Domain;

namespace WomenSafetySystemApi.Repositories;

public class SQLContactRepository : IContactRepository
{
    private readonly WSSDbContext dbContext;
    public SQLContactRepository(WSSDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    //GET ALL Action Method Implementation
    public async Task<List<ContactInfo>> GetAllAsync()
    {
        return await dbContext.Contacts.ToListAsync();
    }

    //POST Action Method Implementation
    public async Task<ContactInfo> CreateAsync(ContactInfo contactInfo)
    {
        await dbContext.Contacts.AddAsync(contactInfo);
        await dbContext.SaveChangesAsync();
        return contactInfo;
    }

    //PUT Action Method Implementation
    public async Task<ContactInfo?> UpdateAsync(Guid id, ContactInfo contactInfo)
    {
        var existingContact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

        if (existingContact == null)
        {
            return null;
        }

        existingContact.Name = contactInfo.Name;
        existingContact.Email = contactInfo.Email;
        existingContact.Phone = contactInfo.Phone;
        existingContact.Address = contactInfo.Address;
        await dbContext.SaveChangesAsync();
        
        return existingContact;
    }

    //DELETE Action Method Implementation
    public async Task<ContactInfo?> DeleteAsync(Guid id)
    {
        var existingContact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

        if (existingContact == null)
        {
            return null;
        }

        dbContext.Contacts.Remove(existingContact);
        await dbContext.SaveChangesAsync();

        return existingContact;
    }
}