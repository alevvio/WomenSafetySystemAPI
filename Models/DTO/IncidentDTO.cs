namespace WomenSafetySystemApi.Models.DTO;

public class IncidentDTO
{
    public Guid Id { get; set; }
    public string Subject { get; set;}
    public string Details { get; set;}
    public bool Resolved { get; set; }
}