using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    IEnumerable<Category> FindAll();
    Category FindId(int id);
    CategoryCreateDto CreateCategory(CategoryCreateDto CategoryDto);
    Category DeleteCategory(int id);
    CategoryUpdateDto UpdateCategory(int id, JsonPatchDocument<CategoryUpdateDto> categoryDto);
}
