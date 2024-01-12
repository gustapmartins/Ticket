using AutoMapper;
using Ticket.DTO.FeatureToggle;
using Ticket.Model;

namespace Ticket.Mapper;

public class FeatureToggleMapper: Profile
{
    public FeatureToggleMapper()
    {
        CreateMap<FeatureToggleCreateDto, FeatureToggle>();
    }
}
