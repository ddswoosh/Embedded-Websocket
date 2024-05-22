using Server.Controllers;

namespace Server.Models;

public class Authorization {
    
    public User _user;
    public Authorization(User user) {
        _user = user;
    }

    public int checkLevel() {
        switch (_user.Type) {
            case "user":
                return 1;
            case "administrator":
                return 2;
            case "microcontroller":
                return 3;
        }

        return 0;
    }
}