using System.ComponentModel;
using System.Drawing;
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
    public string API {get; set;}
}

public class DbConnect
{
    private User user;
    public async Task<User?> GetUser(string username, string password)
    {
        var conn = new SqlConnection(@"Server=PC\SQLEXPRESS;Database=Embedded;Trusted_Connection=True;TrustServerCertificate=True");
        await conn.OpenAsync();

        var query = new SqlCommand(
            $"SELECT * FROM auth WHERE (Username = 'admin' AND Password = 'initroot1234')",
            conn);

        var res = await query.ExecuteReaderAsync();
    
        while (await res.ReadAsync()) 
        {
           if (res["Username"] != null && res["Password"] != null)
           {    
                user.Username = res["Username"].ToString();
                user.Password = res["Password"].ToString();
                user.Type = res["Type"].ToString();
                user.API = res["API"].ToString();
                
                return user;
           }   
        }
        return null;
    }

    public async void PutUser(string username, string password, string type, int API)
    {
        await using var conn = new SqlConnection(@"Server=PC\SQLEXPRESS;Database=Embedded;Trusted_Connection=True;TrustServerCertificate=True");
        await conn.OpenAsync();

        var query = new SqlCommand(
            $"INSERT INTO auth (Username, Password, Type, API) VALUES ({username}, ${password}, ${type}, ${API})", 
            conn);

        try {
            await query.ExecuteReaderAsync(); 
        } catch {
            Console.WriteLine("Query post error");
        }
    }
}