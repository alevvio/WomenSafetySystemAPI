namespace WomenSafetySystemApi.Models.Domain
{
    public class IncidentInfo
    {
        public Guid Id { get; set; }
        public string Subject { get; set;}
        public string Details { get; set;}
        public bool Resolved { get; set; }
    }
}