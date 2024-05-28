using System.Data;
using System.Drawing;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        await using var conn = new SqlConnection(@"Server=PC\SQLEXPRESS;Database=Embedded;Trusted_Connection=True;TrustServerCertificate=True");
        await conn.OpenAsync();
  
        var query = new SqlCommand(
            $"SELECT * FROM auth WHERE Username = '{username}' AND Password = '{password}'",
            conn);
 
        var res = await query.ExecuteReaderAsync();
          
        try 
        {
            while (res.ReadAsync() != null) 
            {   
                user.Username = res.GetString(0);
                user.Password = res.GetString(1);
                user.Type = res.GetString(2);
                user.API = res.GetString(3);
                return user;
            }
        }
         catch (Exception)
        {
            return null;
        }

        return null;
    }

    public async Task<string> PutUser(string username, string password, string type, string API)
    {
        await using var conn = new SqlConnection(@"Server=PC\SQLEXPRESS;Database=Embedded;Trusted_Connection=True;TrustServerCertificate=True");
        await conn.OpenAsync();

        var query = new SqlCommand(
            $"INSERT INTO auth (Username, Password, Type, API) VALUES ('{username}', '{password}', '{type}', '{API}')", 
            conn);

        try 
        {
            var res = await query.ExecuteReaderAsync();
            
            return "Account created";
        } 
        catch (SqlException) 
        {
            return "User already exists";
        }
    }
}