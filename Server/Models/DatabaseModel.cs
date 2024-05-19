namespace Server.Models;

public class User 
{
    public string Username {get; set;}
    public string Password {get; set;}
    public string Type {get; set;}
    public string? API {get; set;}
    public int Auth = 0;

}