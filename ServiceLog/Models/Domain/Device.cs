namespace ServiceLog.Models.Domain
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Short_id { get; set; }
        public string Serial_number { get; set; }
        public string Designation { get; set; }
        public string? Location { get; set; }
        public Guid Category_id { get; set; }
        public string Status { get; set; }
    }
}
