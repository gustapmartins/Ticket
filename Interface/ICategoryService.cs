using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    List<Category> FindAll();

    CategoryCreateDTO CreateCategory(CategoryCreateDTO CategoryDto);
}
