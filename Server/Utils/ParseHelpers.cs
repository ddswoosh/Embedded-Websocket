namespace Server.Utils;

public class Parser
{
    public string[] ParseStream(StreamReader body)
    {
        Task<string> buffer = body.ReadToEndAsync();
        string unwrap = buffer.Result;

        int size = unwrap.Length - 1;
        unwrap = unwrap[1..size];

        Dictionary<string, string> json = unwrap
        .Split(',')
        .Select (part  => part.Split(':'))
        .ToDictionary (sp => sp[0], sp => sp[1]);

        string[] user = new string[4];
        int i = 0;

        foreach(KeyValuePair<string, string> temp in json)
        {
            user[i] = temp.Value;
            i++;
        }

        return user;
    }

    public string[] ParseJson(string body)
    {
        Dictionary<string, string> json = body
        .Split(',')
        .Select (part  => part.Split(':'))
        .ToDictionary (sp => sp[0], sp => sp[1]);

        string[] arr = new string[100];
        int i = 0;

        foreach(KeyValuePair<string, string> temp in json)
        {
            arr[i] = temp.Value;
            i++;
        }

        return arr;
    }
}