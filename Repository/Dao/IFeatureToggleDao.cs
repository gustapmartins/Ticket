using Ticket.Repository.Utils;
using Ticket.Model;
using Ticket.DTO.FeatureToggle;

namespace Ticket.Repository.Dao;

public interface IFeatureToggleDao : ICommand<FeatureToggle>, ObjectHandler<FeatureToggle, FeatureToggleUpdateDto>
{
}
