using System;
using System.Collections.Generic;
using PlanetWarsBot;
using System.IO;

public class MyBot
{
    private static Athena bot = new Athena();

    public static void Main()
    {
#if LOCAL
        File.Delete(@"c:\lol.txt");
#endif
        string line = "";
        string message = "";
        int c;
        try
        {
            while ((c = Console.Read()) >= 0)
            {
                switch (c)
                {
                    case '\n':
                        if (line.Equals("go"))
                        {
                            PlanetWars pw = new PlanetWars(message);
                            bot.DoTurn(pw);
                            pw.FinishTurn();
                            message = "";
                        }
                        else
                        {
                            message += line + "\n";
                        }
                        line = "";
                        break;
                    default:
                        line += (char)c;
                        break;
                }
            }
        }
        catch (Exception)
        {
            // Owned.
        }
    }
}

