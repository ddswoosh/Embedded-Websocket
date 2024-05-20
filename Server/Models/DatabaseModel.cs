using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Data.SqlClient;

namespace Server.Models;

public class User 
{
    public string Username {get; set;}
    public string Password {get; set;}
    public string Type {get; set;}
    public string? API {get; set;}
    public int Auth = 0;
}

public class DbConnect
{
    public User _user;
    public DbConnect(User user)
    {
        _user = user;
    }
    public async void GetUser(string username, string password)
    {
        await using var conn = new SqlConnection(@"Server=PC\SQLEXPRESS;Database=Embedded;Trusted_Connection=True;TrustServerCertificate=True");
        await conn.OpenAsync();

        await using var query = new SqlCommand(
            "SELECT * FROM auth WHERE Usernme = ${username} AND Password = ${Password}",
            conn);

        await using var res = await query.ExecuteReaderAsync();
    
        while (await res.ReadAsync()) 
        {
           if (res["Username"] != null && res["Password"] != null)
           {
                _user.Username = res["Username"].ToString();
                _user.Password = res["Password"].ToString();
                _user.Type = res["Type"].ToString();
                _user.API = res["API"].ToString();
           }   
        }
    }

    public async void PutUser(string username, string password, string type, int API)
    {
        await using var conn = new SqlConnection(@"Server=PC\SQLEXPRESS;Database=Embedded;Trusted_Connection=True;TrustServerCertificate=True");
        await conn.OpenAsync();

        await using var query = new SqlCommand(
            "INSERT INTO auth (Username, Password, Type, API) VALUES (${username}, ${password}, ${type}, ${API})", 
            conn);

        await using var res = await query.ExecuteReaderAsync();
    
        while (await res.ReadAsync()) 
        {
            Console.WriteLine(res.ToString());
        }
    }
}