﻿using AutoMapper;
using Ticket.Data;
using Ticket.DTO.Category;
using Ticket.ExceptionFilter;
using Ticket.Interface;
using Ticket.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace Ticket.Service;

public class CategoryService: ICategoryService
{
    private readonly TicketContext _ticketContext;
    private readonly IMapper _mapper;

    public CategoryService(TicketContext ticketContext, IMapper mapper)
    {
        _ticketContext = ticketContext;
        _mapper = mapper;
    }

    public List<Category> FindAll()
    {
        try
        {
            var find = _ticketContext.Categorys.ToList();

            if (find.Count == 0)
            {
                throw new StudentNotFoundException("The list is empty");
            }
            return find;
        }catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Category FindId(int id)
    {
        try
        {
            var categorys = _ticketContext.Categorys.FirstOrDefault(filme => filme.Id == id);

            if (categorys == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            return categorys;
        }
        catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public CategoryCreateDTO CreateCategory(CategoryCreateDTO CategoryDto)
    {
        try
        {
            var categoryExist = _ticketContext.Categorys.FirstOrDefault(category => category.Name == CategoryDto.Name);

            if(categoryExist == null)
            {
                
                Category category = _mapper.Map<Category>(CategoryDto);
                _ticketContext.Categorys.Add(category);
                _ticketContext.SaveChanges();
                return CategoryDto;
            }

            throw new StudentNotFoundException("This value exist");

        }
        catch(Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public Category DeleteCategory(int id)
    {
        try
        {
            var category = _ticketContext.Categorys.FirstOrDefault(category => category.Id == id);
            if (category == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }
            _ticketContext.Remove(category);
            _ticketContext.SaveChanges();
            return category;
        }catch (Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }

    public CategoryUpdateDTO UpdateCategory(int id, JsonPatchDocument<CategoryUpdateDTO> categoryDto)
    {
        try
        {
            // Exemplo hipotético de busca da categoria pelo ID (isso varia de acordo com sua lógica):
            var category = _ticketContext.Categorys.FirstOrDefault(category => category.Id == id);
             
            if (category == null)
            {
                throw new StudentNotFoundException("This value does not exist");
            }

            var categoryView = _mapper.Map<CategoryUpdateDTO>(category);

            categoryDto.ApplyTo(categoryView);

            _mapper.Map(categoryView, category); 
            _ticketContext.SaveChanges();
            return categoryView;
        }
        catch(Exception ex)
        {
            throw new StudentNotFoundException("Error in the request", ex);
        }
    }
}
