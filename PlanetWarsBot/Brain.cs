using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PlanetWarsBot
{
    public class Brain
    {
        public enum Player
        {
            Neutral = 0,
            Me = 1,
            Other = 2
        }

        private readonly PlanetWars pw;
        public Brain(PlanetWars pw)
        {
            this.pw = pw;
        }

        private IList<Fleet> GetIncomingFleets(Planet p)
        {
            List<Fleet> incomingFleets = new List<Fleet>();

            foreach (Fleet item in pw.EnemyFleets())
            {
                if (item.DestinationPlanet() == p.PlanetID())
                {
                    incomingFleets.Add(item);
                }
            }

            foreach (Fleet item in pw.MyFleets())
            {
                if (item.DestinationPlanet() == p.PlanetID())
                {
                    incomingFleets.Add(item);
                }
            }

            incomingFleets.Sort(new FleetDistanceComparer());

            return incomingFleets;
        }

        private static int PredictPlanetGrowth(Planet planet, int turnCount)
        {
            return planet.GrowthRate() * turnCount;
        }

        public List<DefenceContract> GenerateDefenceContracts(Planet p)
        {
            List<DefenceContract> contracts = new List<DefenceContract>();
            IList<Fleet> incomingFleets = GetIncomingFleets(p);

            int menOnPlanet = p.NumShips();
            int turnsGoneBy = 0;

            for (int i = 0; i < incomingFleets.Count; i++)
            {
                int turnAdvancement = (incomingFleets[i].TurnsRemaining() - turnsGoneBy);

                int planetIncreaseAmount = PredictPlanetGrowth(p, turnAdvancement);// incomingFleets[i].TurnsRemaining() - turnsGoneBy)* p.GrowthRate();
#if LOCAL
                Debug.Write("Planet Increase Amount: " + planetIncreaseAmount + " -- Owner: " + incomingFleets[i].Owner());
#endif
                if (menOnPlanet < 0)
                {
                    menOnPlanet -= planetIncreaseAmount;
                }
                else
                {
                    menOnPlanet += planetIncreaseAmount;
                }
                
                turnsGoneBy += turnAdvancement;

#if LOCAL
                //Debug.Write(string.Format("[{3}]: ns {2} tr {0} gr {1}", incomingFleets[i].TurnsRemaining(), p.GrowthRate(), p.NumShips(), QwertyBot.turnNumber));
#endif
                int afterBattle = menOnPlanet;

                if (incomingFleets[i].Owner() != 1)
                {
                    afterBattle -= incomingFleets[i].NumShips();
                }
                else
                {
                    afterBattle += incomingFleets[i].NumShips();
                }
#if LOCAL
                Debug.Write("turn: " + QwertyBot.TurnNumber + " menOnPlanet: " + menOnPlanet + " turnToImpact: " + incomingFleets[i].TurnsRemaining() + " after battle : " + afterBattle);


                //Debug.Write(string.Format("owner = {0} afterbattle = {1}", incomingFleets[i].Owner(), afterBattle));
#endif
                if (afterBattle < 0 && incomingFleets[i].Owner() != 1)
                {
                    //Debug.Write("LOL");
                    contracts.Add(new DefenceContract(Math.Abs( afterBattle), incomingFleets[i].TurnsRemaining(), p));
                }

                menOnPlanet = afterBattle;
            }

            return contracts;
        }
    }
}
