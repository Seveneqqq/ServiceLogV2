namespace ServiceLog.Models.Domain
{
    public class Service_history
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid DeviceId { get; set; }             
        public List<string> Repairs { get; set; }      
        public Guid TechnicanId { get; set; }          
        public string? OtherInformations { get; set; } 
        public string IssueDescription { get; set; }   
    }
}
