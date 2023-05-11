namespace RayanBours.Domain.Model
{
    public class FiscalYear
    {
        private FiscalYear()
        {
            
        }

        public FiscalYear(Guid id, int companyId, DateTime startDateTime, DateTime endDateTime, int duration)
        {
            Id = id;
            CompanyId = companyId;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Duration = duration;
        }

        public Guid Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Duration { get; set; }
    }
}