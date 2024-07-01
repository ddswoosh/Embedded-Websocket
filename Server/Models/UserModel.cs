using System.ComponentModel.DataAnnotations;

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