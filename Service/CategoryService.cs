using AutoMapper;
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
                throw new StudentNotFoundException("Está lista está vazia");
            }
            return find;
        }catch (Exception ex)
        {
            throw new StudentNotFoundException("Error ao fazer a requisição", ex);
        }
    }

    public CategoryCreateDTO CreateCategory(CategoryCreateDTO CategoryDto)
    {
        try
        {
            Category category = _mapper.Map<Category>(CategoryDto);
            _ticketContext.Categorys.Add(category);
            _ticketContext.SaveChanges();   
            return CategoryDto;
        }catch(Exception ex)
        {
            throw new StudentNotFoundException("Error ao fazer a requisição", ex);
        }
    }

    public Category DeleteCategory(int id)
    {
        try
        {
            var category = _ticketContext.Categorys.FirstOrDefault(endereco => endereco.Id == id);
            if (category == null)
            {
                throw new StudentNotFoundException("Not Found");
            }
            _ticketContext.Remove(category);
            _ticketContext.SaveChanges();
            return category;
        }catch (Exception ex)
        {
            throw new StudentNotFoundException("Error ao fazer a requisição", ex);
        }
    }

    public CategoryUpdateDTO UpdateCategory(int id, JsonPatchDocument<CategoryUpdateDTO> updateDto)
    {
        var category = _ticketContext.Categorys.FirstOrDefault(filme => filme.Id == id);
        if (category == null)
        {
            throw new StudentNotFoundException("Not Found");
        }

        var updateMapper = _mapper.Map<CategoryUpdateDTO>(category);

        _mapper.Map(updateDto, category);
        _ticketContext.SaveChanges();
        return updateMapper;
    }
}
