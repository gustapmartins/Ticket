﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Ticket.Data;
using Ticket.DTO.Show;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;

namespace Ticket.Service;

public class ShowService : IShowService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public ShowService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public IEnumerable<Show> FindAll()
    {
        try
        {
            var find = _ticketContext.Shows.ToList();

            if (find.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return find;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Show FindId(int id)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(category =>
                category.Id == id);

            if (show == null)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public ShowCreateDto CreateShow(ShowCreateDto showDto)
    {
        var category = _ticketContext.Categorys.FirstOrDefault(category =>
            category.Name == showDto.CategoryName);

        if (category == null)
        {
            throw new StudentNotFoundException("A categoria especificada não existe.");
        }
        // Crie o novo Show com a categoria existente
        var show = new Show
        {
            Name = showDto.Name,
            Description = showDto.Description,
            Price = showDto.Price,
            Date = showDto.Date,
            Local = showDto.Local,
            Category = category // Associate the existing category with the Show
        };

        _ticketContext.Shows.Add(show);
        _ticketContext.SaveChanges();
        return showDto;
    }

    public Show DeleteShow(int Id)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(show => show.Id == Id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }
            _ticketContext.Remove(show);
            _ticketContext.SaveChanges();
            return show;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public ShowUpdateDto UpdateShow(int id, JsonPatchDocument<ShowUpdateDto> showDto)
    {
        try
        {
            var show = _ticketContext.Shows.FirstOrDefault(show => show.Id == id);

            if (show == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var categoryView = _mapper.Map<ShowUpdateDto>(show);

            showDto.ApplyTo(categoryView);

            _mapper.Map(categoryView, show);
            _ticketContext.SaveChanges();
            return categoryView;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}