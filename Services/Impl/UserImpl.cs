using Microsoft.EntityFrameworkCore;
using PlantNestBackEnd.Models;
using System.Diagnostics;

namespace PlantNestBackEnd.Services.Impl;

public class UserImpl : IUser
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public UserImpl(DatabaseContext db, IConfiguration configuration) {
        this.db = db;
        this.configuration = configuration;
    }
    public Task<bool> changePassword(string id, string newPass)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> create(Account account)
    {
        try
        {
            db.Accounts.Add(account);
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> delete(int id)
    {
        try
        {
            db.Accounts.Remove(db.Accounts.Find(id));
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> existEmail(string email)
    {
        try
        {
            return (await db.Accounts.Where(a => a.Email == email).CountAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> existUserName(string username)
    {
        try
        {
            return (await db.Accounts.Where(a => a.Username == username).CountAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<dynamic> findAll()
    {
        try
        {
            return (await db.Accounts.Select(a=> new
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
            }).ToListAsync());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<dynamic> findByID(int id)
    {
        try
        {
            return (await db.Accounts.Where(a=>a.Id == id).Select(a => new
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
            }).SingleOrDefaultAsync());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<dynamic> findByUserName(string username)
    {
        try
        {
            return (await db.Accounts.Where(a => a.Username == username).Select(a => new
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
            }).SingleOrDefaultAsync());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> login(string userName, string password)
    {
        try
        {
            var data = db.Accounts.Where(a=>a.Username == userName).SingleOrDefault();
            if (BCrypt.Net.BCrypt.Verify(password, data.Password))
            {
                return  true;
            }
            else
            {
                return  false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> sendCode(string username)
    {
        return false;
    }

    public async Task<bool> update(Account account)
    {
        try
        {
            db.Entry(account).State = EntityState.Modified;
            return (await db.SaveChangesAsync()) > 0;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> verify(string email, string code)
    {
        try
        {
            //var data = db.Accounts.Where(a => a.Email == email).SingleOrDefault();
            //if (code == data.se))
            //{
                return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }
}
