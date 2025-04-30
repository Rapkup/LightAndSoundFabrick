using SemataryFabrick.Domain.Entities.Models.Order.Order;
using SemataryFabrick.Domain.Entities.Models.Users;

namespace SemataryFabrick.Domain.Entities.Models.UserModels;
public class LegalCustomer : ApplicationUser
{
    public string CompanyName { get; set; }
    public string TaxIdNumber { get; set; }
    public string LegalAddress { get; set; }
    public string ContactPersonFullName { get; set; }
    public bool? isGovernment { get; set; }
    public string? GovernmentCode { get; set; }

    public IEnumerable<OrderBase> OrderBases { get; set; }
}
