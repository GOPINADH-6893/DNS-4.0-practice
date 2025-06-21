using System;
public class index
{
    public static void Main(string[] args)
    {
        Logger logger1 = Logger.GetInstance();
        logger1.print("First message");

        Logger logger2 = Logger.GetInstance();
        logger2.print("Second message");

        if (logger1 == logger2)
        {
            Console.WriteLine("single instance used");
        }
        else
        {
            Console.WriteLine("Different instances ");
        }
    }
}