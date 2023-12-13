using Ticket.Enum;

namespace Ticket.DTO.FeatureToggle;

public class FeatureToggleUpdateDto
{
    public string? Name { get; set; }

    public FeatureToggleActive IsEnabledFeature { get; set; }

    public string? Description { get; set; }
}
