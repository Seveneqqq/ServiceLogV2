namespace ServiceLog.Filters
{
    public class TicketFilter
    {
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? ClientId { get; set; }
        public string? TechnicanId { get; set; }
        public string? ReceivingMethod { get; set; }
        public string? ReturnMethod { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
    }
}
