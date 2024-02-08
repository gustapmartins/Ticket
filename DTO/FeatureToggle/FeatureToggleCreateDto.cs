
using ServiceStack.DataAnnotations;
using Ticket.Enum;

namespace Ticket.DTO.FeatureToggle;

public class FeatureToggleCreateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public FeatureToggleActive IsEnabledFeature {  get; set; }

    [Required]
    public string Description { get; set; }
}
