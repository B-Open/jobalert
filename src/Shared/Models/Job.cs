using Shared.Enums;

namespace Shared.Models
{
    public class Job : BaseModel
    {
        public long ProviderId { get; set; }
        public string ProviderJobId { get; set; }
        public long CompanyId { get; set; }
        public string Title { get; set; }
        public string Salary { get; set; }
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public SalaryType SalaryType { get; set; }
        public string Location { get; set; } // TODO: add function to deserialise JSON
        public string Description { get; set; }

        public Provider Provider { get; set; }
        public Company Company { get; set; }
    }
}
