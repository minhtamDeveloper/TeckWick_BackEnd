using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Helplers;
using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl;

public class UserImpl : IUser
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public UserImpl(DatabaseContext db, IConfiguration configuration)
    {
        this.db = db;
        this.configuration = configuration;
    }
    public bool changePassword(string id, string newPass)
    {
        try
        {
            var account = db.Accounts.Find(id);
            account.Password = newPass;
            db.Entry(account).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool create(Account account)
    {
        try
        {
            db.Accounts.Add(account);
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public Account dataLoginSuccessful(string username)
    {
        var data = new Account();
        var find = db.Accounts.Where(a => a.Username == username).SingleOrDefault();
        return find;
    }

    public bool delete(int id)
    {
        try
        {
            db.Accounts.Remove(db.Accounts.Find(id));
            return db.SaveChanges()>0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool existEmail(string email)
    {
        try
        {
            return db.Accounts.Where(a => a.Email == email).Count() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool existUserName(string username)
    {
        try
        {
            return db.Accounts.Where(a => a.Username == username).Count() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public dynamic findAll()
    {
        try
        {
            return db.Accounts.Select(a => new
            {
                id = a.Id,
                fullName = a.Fullname,
                userName = a.Username,
                email = a.Email,
                phone = a.Phone,
                address = a.Address,
                accountImage = a.AccountImage,
                roleId = a.RoleId,
                roleName = a.Role.RoleName,
                created = a.Created,
                dob = a.Dob
            }).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public dynamic findByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public dynamic findByID(int id)
    {
        try
        {
            return db.Accounts.Where(a => a.Id == id).Select(a => new
            {
                id = a.Id,
                fullName = a.Fullname,
                userName = a.Username,
                email = a.Email,
                phone = a.Phone,
                address = a.Address,
                accountImage = a.AccountImage,
                roleId = a.RoleId,
                roleName = a.Role.RoleName,
                created = a.Created,
                dob = a.Dob
            }).SingleOrDefault();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public dynamic findByUserName(string username)
    {
        try
        {
            return db.Accounts.Where(a => a.Username == username).Select(a => new
            {
                id = a.Id,
                fullName = a.Fullname,
                userName = a.Username,
                email = a.Email,
                phone = a.Phone,
                address = a.Address,
                accountImage = a.AccountImage,
                roleId = a.RoleId,
                roleName = a.Role.RoleName,
                created = a.Created.Value.ToString("dd-MM-yyyy"),
                dob = a.Dob.Value.ToString("dd-MM-yyyy")
            }).SingleOrDefault();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool login(string userName, string password)
    {
        try
        {
            var data = db.Accounts.Where(a => a.Username == userName).SingleOrDefault();
            if(data!=null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, data.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool sendCode(string email)
    {
        try
        {
            if (db.Accounts.Where(a => a.Email == email).Count() > 0)
            {
                var mailHelper = new MailHelper(configuration);
                var codeRandom = RandomHelper.RandomInt(6);
                var content = "<h4>Please preserve the subject! This is important for correct mail handling.</h4>"
                               + "<h3>Dear customer</h3>"
                               + "<h5>In order to continue your registrations with Somee International please use the following validation code: </h5>"
                               + "<h2>" + codeRandom + "</h2>"
                               + "<h4>Regards</h4>";
                var check = mailHelper.Send(configuration["Gmail:Username"], email, "Re:{Plant Nest} Email verification", content);
                if (check)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool update(Account account)
    {
        try
        {
            db.Entry(account).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public bool verify(string email, string code)
    {
        try
        {
            var data = db.Accounts.Where(a => a.Email == email).SingleOrDefault();
            if (data.SercurityCode == code)
            {
                return true;
            }
            else { return false; }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
