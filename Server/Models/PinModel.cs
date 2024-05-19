namespace Server.Models;

public interface PinInterface
{
    void PutCurrentPin(string id);
    string GetCurrentPin();
}

public class PinLive : PinInterface
{
    public string pin = "/";

    public void PutCurrentPin(string id)
    {   
        pin = id;
    }

    public string GetCurrentPin()
    {
        return pin;
    }
}