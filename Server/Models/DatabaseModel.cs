using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public class UserContext : DbContext
{   
    private DbContextOptions<UserContext> context;
    public UserContext(DbContextOptions<UserContext> db) : base(db)
    {
        this.context = db;
    }

    public DbSet<User> Users {get; set;}

    public bool SetUser(string[] user, User entity)
    {
        using (var db = new UserContext(context))
        {  

        if (
            db.Users.Where(u => u.Username == user[0]).ToArray().Length > 0 ||
            db.Users.Where(u => u.Username == user[3]).ToArray().Length > 0
        )
        {   
            return false;
        }
        
        db.Users.Add(entity);
        db.SaveChanges();

        return true;
        }
    }

    public User[]? GetUser(string[] user)
    {
        using (var db = new UserContext(context))
        {  
            User[] entity = db.Users.Where(u => u.Username == user[0]).Where(p => p.Password == user[1]).ToArray();

            if (entity.Length == 0)
            {   
                return null;
            }

            return entity;
        }
    }
}