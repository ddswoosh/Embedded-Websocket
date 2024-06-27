using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Server.Models;

public class User
{   
    [Key]
    public int id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public string Type {get; set;}
    public string API {get; set;}

}

public class ID 
{
    public int id {get; set;}
}