using Microsoft.AspNetCore.JsonPatch;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Interface;

public interface ICategoryService
{
    List<Category> FindAllCategory();
    Category FindIdCategory(string id);
    CategoryCreateDto CreateCategory(CategoryCreateDto CategoryDto);
    Category DeleteCategory(string id);
    Category UpdateCategory(string id, CategoryUpdateDto categoryDto);
}
