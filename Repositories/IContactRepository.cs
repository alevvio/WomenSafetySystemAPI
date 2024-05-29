using WomenSafetySystemApi.Models.Domain;

namespace WomenSafetySystemApi.Repositories;

public interface IContactRepository
{
    //Contacts Controller
        //GETALL Action Method
        Task<List<ContactInfo>> GetAllAsync();
        //POST Action Method
        Task<ContactInfo> CreateAsync(ContactInfo contactInfo);
        //PUT Action Method
        Task<ContactInfo?> UpdateAsync(Guid Id, ContactInfo contactInfo);
        //DELETE Action MEthod
        Task<ContactInfo?> DeleteAsync(Guid Id);
}