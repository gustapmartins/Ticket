using AutoMapper;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Mapper;

public class CategoryMapper: Profile
{
    public CategoryMapper() 
    {
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryUpdateDto>();
    }    
}
