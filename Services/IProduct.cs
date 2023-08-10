using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface IProduct
{
    Task<dynamic> findAll();
    Task<dynamic> findByID(int id);
    Task<bool> create(Product product);
    Task<bool> update(Product product);
    Task<bool> delete(int id);
    Task<bool> exist (int id);
}
