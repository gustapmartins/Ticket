using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    List<Category> FindAll();
    Category FindId(int id);
    CategoryCreateDTO CreateCategory(CategoryCreateDTO CategoryDto);
    Category DeleteCategory(int id);
    CategoryUpdateDTO UpdateCategory(int id, JsonPatchDocument<CategoryUpdateDTO> categoryDto);
}
