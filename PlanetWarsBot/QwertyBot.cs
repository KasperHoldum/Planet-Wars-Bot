using System;
using System.Collections.Generic;
using System.IO;

namespace PlanetWarsBot
{
    public class QwertyBot
    {
        public static int TurnNumber;

        public void DoTurn(PlanetWars pw)
        {
            TurnNumber++;

            try
            {
                SetupDefence(pw);
                SetupOffence(pw);
            }
            catch (Exception)
            {

                // Exception!
            }
        }

        private static void SetupOffence(PlanetWars pw)
        {
            if (pw.MyFleets().Count >= 1)
            {
                return;
            }

            // (2) Find my strongest planet.
            Planet source = null;
            double sourceScore = Double.MinValue;
            foreach (Planet p in pw.MyPlanets())
            {
                double score = (double)p.NumShips();
                if (score > sourceScore)
                {
                    sourceScore = score;
                    source = p;
                }
            }
            // (3) Find the weakest enemy or neutral planet.
            Planet dest = null;
            double destScore = Double.MinValue;
            foreach (Planet p in pw.NotMyPlanets())
            {
                double score = 1.0 / (1 + p.NumShips());
                if (score > destScore)
                {
                    destScore = score;
                    dest = p;
                }
            }
            // (4) Send half the ships from my strongest planet to the weakest
            // planet that I do not own.
            if (source != null && dest != null)
            {
                int numberOfShipsToAttackWith = source.NumShips() / 4 *3;

                if (IsValidMove(source, numberOfShipsToAttackWith))
                    pw.IssueOrder(source, dest, numberOfShipsToAttackWith);
            }
        }

        public static bool IsValidMove(Planet source, int numberOfShipsToMove)
        {
            return source.Owner() == (int) Brain.Player.Me && source.NumShips() >= numberOfShipsToMove;
        }

        private static void SetupDefence(PlanetWars pw)
        {
            Brain b = new Brain(pw);
            Dictionary<Planet, List<DefenceContract>> contracts = new Dictionary<Planet, List<DefenceContract>>();

            foreach (Planet p in pw.MyPlanets())
            {
                contracts[p] = b.GenerateDefenceContracts(p);
            }

            foreach (Planet p in pw.MyPlanets())
            {
                List<DefenceContract> planetContracts = contracts[p];

                foreach (DefenceContract item in planetContracts)
                {
                    // Debug.Write(item);
#if LOCAL
                    File.AppendAllText(@"c:\lol.txt", item + Environment.NewLine);
#endif
                    foreach (Planet supportPlanet in pw.MyPlanets())
                    {
                        if (supportPlanet.PlanetID() != p.PlanetID())
                        {
                            if (supportPlanet.NumShips() > item.ShipsNeeded && item.TimeFrame <= (supportPlanet.GetVector().DistanceTo(p.GetVector())))
                            {

                                if (IsValidMove(supportPlanet, item.ShipsNeeded))
                                {
                                    pw.IssueOrder(supportPlanet, p, item.ShipsNeeded);

                                    supportPlanet.NumShips(supportPlanet.NumShips() - item.ShipsNeeded);
                                }

                                break;
                            }
                        }
                    }
                }

            }
        }
    }
}
