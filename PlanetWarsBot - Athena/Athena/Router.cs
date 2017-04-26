using System;
using System.Collections.Generic;
using System.Linq;
namespace AthenaBot
{
    public class Router
    {
        public Router(PlanetWars pw, Athena athena)
        {
            this.PW = pw;
            this.Athena = athena;
        }

        public PlanetWars PW { get; set; }
        public Athena Athena { get; set; }


        /// <summary>
        /// Entry point for the router
        /// </summary>
        public void Route()
        {
            // basic routing stragy
            //  1. Calculate frontline
            //  2. Supply front line with ships

            List<Planet> frontLine = this.ComputeFrontline();

            foreach (Planet myPlanet in PW.MyPlanets())
            {
                if (!frontLine.Contains(myPlanet))
                {
                    // find nearest frontline planet
                    Planet frontlinePlanet = FindClosestPlanet(myPlanet.Location, frontLine);


                    if (frontlinePlanet != null)
                    {
                        int shipsToRoute = Math.Min(myPlanet.NumShips, this.Athena.Oracle.FreeShipsOnPlanet(myPlanet, Player.Me));

                        if (shipsToRoute > 0)
                            this.Athena.IssueOrder(myPlanet, frontlinePlanet, shipsToRoute);
                    }
                }
            }

            // 3. Front line can route to eachother. This is to prevent a large force standing at a frontline planet when enemy COM is at another place
            //frontLine.Sort(new DistanceComparer(this.Athena.FindPlayerCenterOfMass(PW.EnemyFleets(), PW.EnemyPlanets(), true)));

            //for (int i = frontLine.Count - 1; i > 0; i--)
            //{
            //    int shipsToSend = Math.Min(0, frontLine[i].NumShips / 4);

            //    if (shipsToSend > 0)
            //        this.Athena.IssueOrder(frontLine[i], frontLine[i - 1], shipsToSend);
            //}

        }

        private List<Planet> ComputeFrontline()
        {
            List<Planet> frontLine = new List<Planet>();

            foreach (Planet enemyPlanet in PW.EnemyPlanets())
            {
                Planet closestPlanet = FindClosestPlanet(enemyPlanet.Location, (int)Player.Me);

                if (closestPlanet != null)
                {
                    if (!frontLine.Contains(closestPlanet))
                        frontLine.Add(closestPlanet);
                }
            }

            return frontLine;
        }

        private Planet FindClosestPlanet(Vector location, int owner)
        {
            IEnumerable<Planet> planetsToSearch;
            
            switch (owner)
            {
                case (int)Player.Me:
                    planetsToSearch = PW.MyPlanets().Union(this.Athena.Oracle.FutureMePlanets().Select(p => p.Value1));
                    break;
                case (int)Player.Enemy:
                    planetsToSearch = PW.EnemyPlanets();
                    break;
                case (int)Player.Neutral:
                    planetsToSearch = PW.NeutralPlanets();
                    break;
                case -1:
                    planetsToSearch = PW.Planets;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("owner");
            }

            return FindClosestPlanet(location, planetsToSearch);
        }

        private static Planet FindClosestPlanet(Vector location, IEnumerable<Planet> setToSearch)
        {
            Planet p = null;
            int distance = int.MaxValue;
            foreach (Planet planet in setToSearch)
            {
                int newDistance = location.DistanceTo(planet.Location);

                if (newDistance >= distance) continue;

                p = planet;
                distance = newDistance;
            }
            return p;
        }
    }
}
