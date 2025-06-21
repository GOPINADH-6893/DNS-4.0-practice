using System;

public class Logger
{
    private static Logger instance;  
    private Logger()
    {
        Console.WriteLine("Logger has created");
    }
    public static Logger GetInstance()
    {
        if (instance == null)
        {
            instance = new Logger();
        }
        return instance;
    }
    public void print(string message)
    {
        Console.WriteLine("LOG" + message);
    }
}