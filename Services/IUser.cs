using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface IUser
{
    dynamic findAll();
    dynamic findByID(int id);
    Account dataLoginSuccessful(string username);
    dynamic findByUserName(string username);
    dynamic findByEmail(string email);
    bool create(Account account);
    bool update(int id , Account account);
    bool updateAvt(int id, string nameImg);
    bool delete(int id);
    bool existUserName(string username);
    bool existEmail(string email);
     bool sendCode(string username);
     bool login(string userName, string password);
     bool verify(string email,string code);
     bool changePassword(string id, string newPass);
}
