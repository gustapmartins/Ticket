using Ticket.DTO.FeatureToggle;
using Ticket.Model;

namespace Ticket.Interface;

public interface IFeatureToggleService
{
    List<FeatureToggle> FindAllFeatureToggle();
    FeatureToggle FindIdFeatureToggle(string nameFeatureToggle);
    FeatureToggle CreateFeatureToggle(FeatureToggleCreateDto featureToggleCreateDto);
    FeatureToggle DeleteFeatureToggle(string id);
    FeatureToggle UpdateFeatureToggle(string id, FeatureToggleUpdateDto featureToggleUpdateDto);
    bool FeatureToggleActive(string FT_TICKETS);
}
