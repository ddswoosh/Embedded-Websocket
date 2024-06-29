using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public class UserContext : DbContext
{   

    private DbContextOptions<UserContext> _options;
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        _options = options;
    }

    public DbSet<User> Users {get; set;}

    public void Set()
    {
    
        
    }

    public void get(string username, string password)
    {
        var query = from i in Users
                    select i;

        Console.WriteLine(query);
        
    }
}