using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface IAccount
{
    public dynamic showAll();
   
    public dynamic Search(string keyword);

}
