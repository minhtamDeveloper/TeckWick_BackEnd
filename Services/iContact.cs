using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface iContact
{
    public dynamic findAll();
    public dynamic findByID(int id);
    public bool create(Contact contact);
    //public bool update(Account account);
    public bool Delete(int id);
    
}
