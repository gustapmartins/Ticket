using Ticket.DTO.FeatureToggle;
using Ticket.Model;

namespace Ticket.Interface;

public interface IFeatureToggleService
{
    ResultOperation<List<FeatureToggle>> FindAllFeatureToggle();
    ResultOperation<FeatureToggle> FindIdFeatureToggle(string nameFeatureToggle);
    ResultOperation<FeatureToggle> CreateFeatureToggle(FeatureToggleCreateDto featureToggleCreateDto);
    ResultOperation<FeatureToggle> DeleteFeatureToggle(string id);
    ResultOperation<FeatureToggle> UpdateFeatureToggle(string id, FeatureToggleUpdateDto featureToggleUpdateDto);
    bool FeatureToggleActive(string FT_TICKETS);
}
