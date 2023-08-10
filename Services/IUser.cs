using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services;

public interface IUser
{
    Task<dynamic> findAll();
    Task<dynamic> findByID(int id);
    Task<dynamic> findByUserName(string username);
    Task<bool> create(Account account);
    Task<bool> update(Account account);
    Task<bool> delete(int id);
    Task<bool> existUserName(string username);
    Task<bool> existEmail(string email);
    Task<bool> sendCode(string username);
    Task<bool> login(string userName, string password);
    Task<bool> verify(string email,string code);
    Task<bool> changePassword(string id, string newPass);
}
