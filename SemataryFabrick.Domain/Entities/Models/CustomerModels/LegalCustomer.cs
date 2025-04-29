namespace SemataryFabrick.Domain.Entities.Models.Customers;
public class LegalCustomer : Customer
{
    public string CompanyName { get; set; }
    public string TaxIdNumber { get; set; }
    public string LegalAddress { get; set; }
    public string ContactPersonFullname { get; set; }
    public bool IsGovernment { get; set; }
    public string? GovernmentCode { get; set; }
}
