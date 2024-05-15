namespace Server.Models;

public class CurrentPin
{
    public string[] arr = {"null"};

    public string PutCurrentPin(string id)
    {   
        Console.WriteLine(id);
        arr[0] = id; 
        return arr[0];
    }

}