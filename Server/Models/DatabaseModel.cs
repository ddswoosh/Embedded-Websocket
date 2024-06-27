using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

public class UserContext : DbContext
{   
    private ID _id;
    private DbContextOptions<UserContext> _options;
    public UserContext(DbContextOptions<UserContext> options, ID Id) : base(options)
    {
        _id = Id;
        _options = options;
    }

    public DbSet<User> Users {get; set;}

    public void Set(string username, string password, string type, string api)
    {
        using (var context = new UserContext(_options, _id))
        {   
            User user = new User();
            user.id = _id.id;
            user.Username = "testing";
            user.Password = "testing";
            user.Type = "user";
            user.API = api; 

            context.Users.Add(user);
        }
        
    }

    public void get(string username, string password)
    {
        var query = from i in Users
                    select i;

        Console.WriteLine(query);
        
    }
}