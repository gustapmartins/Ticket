using System.ComponentModel.DataAnnotations;
using Ticket.Enum;

namespace Ticket.Model;

public class FeatureToggle
{
    public FeatureToggle() 
    { 
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    public string Id { get; set; }

    public string Name { get; set; }

    public FeatureToggleActive IsEnabledFeature {  get; set; }
    
    public string Description { get; set; }
}
