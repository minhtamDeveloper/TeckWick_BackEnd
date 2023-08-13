using PlantNestBackEnd.Models;

namespace PlantNestBackEnd.Services.Impl;

public class contactImpl : iContact
{
    private DatabaseContext db;
    public contactImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool create(Contact contact)
    {
        try
        {
            db.Contacts.Add(contact);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            db.Contacts.Remove(db.Contacts.Find(id));
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findAll()
    {
        return db.Contacts.OrderByDescending(ct => ct.Id).Select(ct => new
        {
            Id = ct.Id,
            Name = ct.Name,
            Email = ct.Email,
            Subject = ct.Subject,
            Message = ct.Message,
            Created = ct.Created,
        }).ToList();
    }

    public dynamic findByID(int id)
    {
        return db.Contacts.Where(ct => ct.Id == id).Select(ct => new
        {
            Id = ct.Id,
            Name = ct.Name,
            Email = ct.Email,
            Subject = ct.Subject,
            Message = ct.Message,
            Created = ct.Created,
        }).SingleOrDefault();
    }

    //public dynamic findByID(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //public bool update(Account account)
    //{
    //    throw new NotImplementedException();
    //}
}
