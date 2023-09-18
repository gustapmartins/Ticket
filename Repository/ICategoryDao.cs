using Ticket.Data;
using Ticket.DTO.Category;
using Ticket.Model;

namespace Ticket.Repository;

public interface ICategoryDao
{
    IEnumerable<Category> FindAllCategorys();

    Category FindIdCategory(int Id);

    bool CategoryExistName(CategoryCreateDto categoryDto);

    void Add(Category category);

    void Remove(Category category);

    void SaveChanges();
}
