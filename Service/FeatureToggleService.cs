using Ticket.DTO.FeatureToggle;
using Ticket.ExceptionFilter;
using Ticket.Repository.Dao;
using Ticket.Validation;
using Ticket.Interface;
using Ticket.Model;
using AutoMapper;

namespace Ticket.Service;

public class FeatureToggleService : BaseService, IFeatureToggleService
{
    private readonly IFeatureToggleDao _featureToggleDao;
    private readonly IMapper _mapper;

    public FeatureToggleService(IFeatureToggleDao featureToggleDao, IMapper mapper)
    {
        _featureToggleDao = featureToggleDao;
        _mapper = mapper;
    }

    public ResultOperation<FeatureToggle> CreateFeatureToggle(FeatureToggleCreateDto featureToggleDto)
    {
        try
        {
            var findFeatureToggle = _featureToggleDao.FindId(featureToggleDto.Name);

            if (findFeatureToggle != null)
            {
                return CreateErrorResult<FeatureToggle>("This FeatureToggle already exists");
            }

            FeatureToggle featureToggle = _mapper.Map<FeatureToggle>(featureToggleDto);

            _featureToggleDao.Add(featureToggle);

            return CreateSuccessResult(findFeatureToggle);

        }
        catch (Exception ex)
        {
            return CreateErrorResult<FeatureToggle>(ex.Message);
        }
    }

    public ResultOperation<FeatureToggle> DeleteFeatureToggle(string id)
    {
        try
        {
            var findFeatureToggle = _featureToggleDao.FindId(id);

            if(findFeatureToggle == null)
            {
                return CreateErrorResult<FeatureToggle>("This value is not exist");
            }

            _featureToggleDao.Remove(findFeatureToggle);

            return CreateSuccessResult(findFeatureToggle);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<FeatureToggle>(ex.Message);
        }
    }

    public ResultOperation<List<FeatureToggle>> FindAllFeatureToggle()
    {
        try
        {
            List<FeatureToggle> findFeatureToggle = _featureToggleDao.FindAll();

            if (findFeatureToggle.Count == 0)
            {
                return CreateErrorResult<List<FeatureToggle>>("The list is empty");
            }

            return CreateSuccessResult(findFeatureToggle);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<List<FeatureToggle>>(ex.Message);
        }
    }

    public ResultOperation<FeatureToggle> FindIdFeatureToggle(string id)
    {
        try
        {
            var findIdFeatureToggle = _featureToggleDao.FindId(id);

            if (findIdFeatureToggle == null)
            {
                return CreateErrorResult<FeatureToggle>("This value is not exist");
            }

            return CreateSuccessResult(findIdFeatureToggle);
        }catch(Exception ex)
        {
            return CreateErrorResult<FeatureToggle>(ex.Message);
        }
    }

    public ResultOperation<FeatureToggle> UpdateFeatureToggle(string id, FeatureToggleUpdateDto featureToggleUpdateDto)
    {
        try
        {
            var findFeatureToggle = _featureToggleDao.FindId(id);

            if(findFeatureToggle == null)
            {
                return CreateErrorResult<FeatureToggle>("This value is not exist");
            }

            _featureToggleDao.Update(findFeatureToggle, featureToggleUpdateDto);

            return CreateSuccessResult(findFeatureToggle);
        }
        catch (Exception ex)
        {
            return CreateErrorResult<FeatureToggle>(ex.Message);
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
