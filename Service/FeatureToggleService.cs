using Ticket.DTO.FeatureToggle;
using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using Ticket.Interface;
using Ticket.Validation;
using Ticket.Model;
using AutoMapper;

namespace Ticket.Service;

public class FeatureToggleService : TicketBase, IFeatureToggleService
{
    private readonly IFeatureToggleDao _featureToggleDao;
    private readonly IMapper _mapper;

    public FeatureToggleService(IFeatureToggleDao featureToggleDao, IMapper mapper)
    {
        _featureToggleDao = featureToggleDao;
        _mapper = mapper;
    }

    public FeatureToggle CreateFeatureToggle(FeatureToggleCreateDto featureToggleDto)
    {
        try
        {
            var findFeatureToggle = _featureToggleDao.FindId(featureToggleDto.Name);

            if (findFeatureToggle != null)
            {
                throw new StudentNotFoundException("This FeatureToggle already exists");
            }

            FeatureToggle featureToggle = _mapper.Map<FeatureToggle>(featureToggleDto);

            _featureToggleDao.Add(featureToggle);

            return featureToggle;

        }
        catch (Exception ex)
        {
            throw new NotImplementedException($"Error in the request: {ex}");
        }
    }

    public FeatureToggle DeleteFeatureToggle(string id)
    {
        try
        {
            var findFeatureToggle = HandleErrorAsync(() => _featureToggleDao.FindId(id));

            _featureToggleDao.Remove(findFeatureToggle);

            return findFeatureToggle;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public List<FeatureToggle> FindAllFeatureToggle()
    {
        try
        {
            List<FeatureToggle> findFeatureToggle = _featureToggleDao.FindAll();

            if (findFeatureToggle.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }

            return findFeatureToggle;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public FeatureToggle FindIdFeatureToggle(string id)
    {
        return HandleErrorAsync(() => _featureToggleDao.FindId(id));
    }

    public FeatureToggle UpdateFeatureToggle(string id, FeatureToggleUpdateDto featureToggleUpdateDto)
    {
        try
        {
            var findFeatureToggle = HandleErrorAsync(() => _featureToggleDao.FindId(id));

            _featureToggleDao.Update(findFeatureToggle, featureToggleUpdateDto);

            return findFeatureToggle;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public bool FeatureToggleActive(string FT_TICKETS)
    {
        try
        {
            var findFeatureToggleRedis = _featureToggleDao.FindId(FT_TICKETS);

            return findFeatureToggleRedis != null && findFeatureToggleRedis.IsEnabledFeature == Enum.FeatureToggleActive.active;
        }
        catch
        {
            return false;
        }
    }
}
