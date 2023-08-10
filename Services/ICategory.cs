using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface ICategory
{
    Task<dynamic> findAll();
    Task<dynamic> findByID(int id);
    Task<bool> create(Category category);
    Task<bool> update(Category category);
    Task<bool> delete(int id);
    Task<bool> exist(int id);
}
