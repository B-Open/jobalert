namespace Shared.Models
{
    public class Company : BaseModel
    {
        public long ProviderId { get; set; }
        public string ProviderCompanyId { get; set; }
        public string Name { get; set; }
    }
}
