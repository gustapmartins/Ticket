using Ticket.DTO.FeatureToggle;
using Ticket.Repository.Dao;
using Ticket.ExceptionFilter;
using Ticket.Data;
using Ticket.Model;
using ServiceStack;

namespace Ticket.Repository.EfCore;

public class FeatureToggleEfCore : IFeatureToggleDao
{
    private readonly TicketContext _ticketContext;
    public FeatureToggleEfCore(TicketContext ticketContext)
    {
        _ticketContext = ticketContext;
    }

    public void Add(FeatureToggle featureToggle)
    {
        _ticketContext.FeatureToggles.Add(featureToggle);
        _ticketContext.SaveChanges();
    }

    public List<FeatureToggle> FindAll()
    {
       return _ticketContext.FeatureToggles.ToList();
    }

    public FeatureToggle FindId(string nameOrId)
    {
        return _ticketContext.FeatureToggles.FirstOrDefault(featureToggle => featureToggle.Id == nameOrId || featureToggle.Name == nameOrId)!;
    }

    public void Remove(FeatureToggle featureToggle)
    {
        _ticketContext.FeatureToggles.Remove(featureToggle);
        _ticketContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _ticketContext.SaveChanges();
    }

    public void Update(FeatureToggle featureToggle, FeatureToggleUpdateDto featureToggleCreateAndUpdate)
    {
        if (featureToggleCreateAndUpdate.Name != null)
        {
            featureToggle.Name = featureToggleCreateAndUpdate.Name;
        }
        if(featureToggleCreateAndUpdate.IsEnabledFeature != null)
        {
            featureToggle.IsEnabledFeature = featureToggleCreateAndUpdate.IsEnabledFeature;
        }
        if (featureToggleCreateAndUpdate.Description != null)
        {
            featureToggle.Description = featureToggleCreateAndUpdate.Description;
        }

        _ticketContext.SaveChanges();
    }
}
