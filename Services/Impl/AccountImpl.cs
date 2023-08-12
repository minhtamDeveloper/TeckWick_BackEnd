using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services.Impl;

public class AccountImpl : IAccount
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public AccountImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public dynamic Search(string keyword)
    {
        return db.Accounts.Where(a => a.Username.Contains(keyword)).Select(a => new
        {
            accountId = a.Id,
            userName = a.Username,
            fullName = a.Fullname,
            email = a.Email,
            phone = a.Phone,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "img-profile/" + a.AccountImage,
            roleId = a.RoleId,
            created = a.Created,
            dob = a.Dob,
        }).ToList();
    }

    public dynamic showAll()
    {
        return db.Accounts.OrderByDescending(a => a.Id).Select(a => new
        {
            accountId = a.Id,
            userName = a.Username,
            fullName = a.Fullname,
            email = a.Email,
            phone = a.Phone,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "img-profile/" + a.AccountImage,
            roleId = a.RoleId,
            created = a.Created,
            dob = a.Dob,
            status = a.Status
        }).ToList();
    }

 
}
