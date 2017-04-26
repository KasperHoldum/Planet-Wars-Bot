using System;
using System.Text;
using AthenaBot;

public class MyBot
    {
        private static readonly Athena Bot = new Athena();

        public static void Main()
        {
            string line = "";


            StringBuilder message = new StringBuilder();

            try
            {
                int c;
                while ((c = Console.Read()) >= 0)
                {
                    switch (c)
                    {
                        case '\n':
                            if (line.Equals("go"))
                            {
                                PlanetWars pw = new PlanetWars(message.ToString());
                                Bot.DoTurn(pw);
                                PlanetWars.FinishTurn();
                                message = new StringBuilder();
                            }
                            else
                            {
                                message.Append(line + "\n");
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

