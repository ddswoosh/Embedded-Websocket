using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Server.Models;

public class Authorization : IAuthorizationRequirement
{
    public int Level {get;}
    public Authorization(string? type) {
        if (type == "user") {
            Level = 1;
        } else if (type == "administrator") {
            Level = 2;
        } else if (type == "chip") {
            Level = 3;
        } else {
            Level = 0;
        }
    }   
}