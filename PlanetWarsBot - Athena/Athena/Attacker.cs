using System;
using System.Collections.Generic;

namespace AthenaBot
{
    public class Attacker
    {
        private bool HasGoneAllOut{ get; set;}

        private readonly Planet enemyStartPlanet;
        private readonly Planet meStartPlanet;

        public PlanetWars PW { get; set; }
        public Athena Athena { get; set; }

        public Attacker(PlanetWars pw, Athena athena)
        {
            this.PW = pw;
            this.Athena = athena;

            this.meStartPlanet = pw.MyPlanets()[0];
            this.enemyStartPlanet = pw.EnemyPlanets()[0]; 
        }


        /// <summary>
        /// Entry point for the attack routine
        /// </summary>
        public void Attack()
        {
            int timeBetweenCenterOfMass = this.Athena.TimeBetweenPlayersCenterOfMass();

            if (this.Athena.MyShipCount + this.Athena.MyFleetCountNotLostToNeutrals()
                        >
                        this.Athena.EnemyShipCount + this.Athena.EnemyFleetCountNotLostToNeutrals() +
                            (this.Athena.EnemyShipProduction + 4) * timeBetweenCenterOfMass || HasGoneAllOut) // go all out scenario
            {
                SendAllShipsToNearestEnemyPlanet();
                HasGoneAllOut = true;
            }
            else if (this.Athena.ShipRatio > 1.0f & this.Athena.ProductionRatio > 1.0f) // trade 1:1 scenario
            {
                OptimizeProductionByAttacking();
                SendAFewShipsToNearestEnemyPlanet();
            }
            else // start looking for attractive neutral planets closeby
            {
                bool attackedEverythingWeDesired = OptimizeProductionByAttacking();

                if (attackedEverythingWeDesired)
                {
                    SendAllShipsToNearestEnemyPlanet();
                    HasGoneAllOut = true;
                }
            }
        }

        private bool OptimizeProductionByAttacking()
        {
            // 1. Find distance, in turns, to opponent

            int turnDistanceToOpponent = this.Athena.TimeBetweenPlayersCenterOfMass();
            Vector myCoM = this.Athena.FindPlayerCenterOfMass(this.PW.MyFleets(), PW.MyPlanets(), true);


            // 2. Find all planets that after above number of turns would result in a gained amount of troops
            IEnumerable<Planet> planetsWorthAttacking = this.Athena.Turn == 1 ? FindTurnOnePlanetsWorthConquering(turnDistanceToOpponent) : FindPlanetsWorthConquering(myCoM, turnDistanceToOpponent);

            // 3. Max the amount of production among the above planets
            List<Tuple<int, Planet>> gainFromConqueringPlanets = MaxProductionAmongPlanets(myCoM, turnDistanceToOpponent, planetsWorthAttacking);


            if (gainFromConqueringPlanets.Count == 0)
                return true;

            // 4. conquer according to the last computed above
            int turnOneBudget = turnDistanceToOpponent * this.Athena.MyShipProduction;
            foreach (Tuple<int, Planet> t in gainFromConqueringPlanets)
            {
                if (this.Athena.Turn == 1)
                {
                    if (t.Value2.NumShips + 1 <= turnOneBudget){
                        AttackPlanet(t.Value2);
                        turnOneBudget -= t.Value2.NumShips + 1;
                    }
                }
                else if (this.Athena.Oracle.TotalFreeShips(Player.Me) > t.Value2.NumShips + 1) // 1 extra ship to conquer the planet
                {
                    AttackPlanet(t.Value2);
                }
            }

            return false;
        }

        private IEnumerable<Planet> FindTurnOnePlanetsWorthConquering(int turnDistanceToOpponent)
        {
            int budget = turnDistanceToOpponent*this.Athena.MyShipProduction;

            List<Planet> planetsWorthAttacking = new List<Planet>();

            foreach (Planet notMyPlanet in PW.NotMyPlanets())
            {
                int distanceToMe = this.Athena.PlanetDistances[notMyPlanet.Id][meStartPlanet.Id];
                int distanceToEnemy = this.Athena.PlanetDistances[notMyPlanet.Id][enemyStartPlanet.Id];

                if (this.Athena.Oracle.IsPlayerFutureOwner(notMyPlanet, Player.Me))
                    continue;

                if (distanceToEnemy <= distanceToMe)
                    continue;
                
                if (notMyPlanet.NumShips < budget)
                {
                    planetsWorthAttacking.Add(notMyPlanet);
                }
            }
            return planetsWorthAttacking;
        }

        private static List<Tuple<int, Planet>> MaxProductionAmongPlanets(Vector myCoM, int turnDistanceToOpponent, IEnumerable<Planet> planetsWorthAttacking)
        {
            List<Tuple<int, Planet>> gainFromConqueringPlanets = new List<Tuple<int, Planet>>();

            foreach (Planet notMyPlanet in planetsWorthAttacking)
            {
                int distanceBetweenCoMAndPlanet = notMyPlanet.Location.DistanceTo(myCoM);
                int planetGrowthRate = notMyPlanet.GrowthRate;
                int shipsOnPlanet = notMyPlanet.NumShips;

                int turnsWhereWeGetGrowthFromPlanet = turnDistanceToOpponent - distanceBetweenCoMAndPlanet;
                int growthGainedFromPlanet = planetGrowthRate * turnsWhereWeGetGrowthFromPlanet;


                int totalShipsGained = growthGainedFromPlanet - shipsOnPlanet;

                // we gain even more if the planet is owned by opponent
                if (notMyPlanet.Owner == Player.Enemy)
                {
                    totalShipsGained += growthGainedFromPlanet;
                }

                gainFromConqueringPlanets.Add(new Tuple<int, Planet>(totalShipsGained, notMyPlanet));
            }

            // sort list
            gainFromConqueringPlanets.Sort(new GainFromConquestComparer());
            return gainFromConqueringPlanets;
        }

        private IEnumerable<Planet> FindPlanetsWorthConquering(Vector myCoM, int turnDistanceToOpponent)
        {
            List<Planet> planetsWorthAttacking = new List<Planet>();

            foreach (Planet notMyPlanet in PW.NotMyPlanets())
            {
                if (this.Athena.Oracle.IsPlayerFutureOwner(notMyPlanet, Player.Me))
                    continue; // don't take planets we already own (in the future)
                if (this.Athena.PlanetDistances[notMyPlanet.Id][meStartPlanet.Id] >= this.Athena.PlanetDistances[notMyPlanet.Id][enemyStartPlanet.Id])
                    continue; // don't take planets on enemy side.

                int distanceBetweenCoMAndPlanet = notMyPlanet.Location.DistanceTo(myCoM);
                int planetGrowthRate = notMyPlanet.GrowthRate;
                int shipsOnPlanet = notMyPlanet.NumShips;
                int turnsWhereWeGetGrowthFromPlanet = turnDistanceToOpponent - distanceBetweenCoMAndPlanet;
                int growthGainedFromPlanet = planetGrowthRate * turnsWhereWeGetGrowthFromPlanet;

                if (growthGainedFromPlanet > shipsOnPlanet)
                {
                    planetsWorthAttacking.Add(notMyPlanet);
                }
            }
            return planetsWorthAttacking;
        }

        /// <summary>
        /// Will send ships from all planets to the planet until it is conquered. The planets closest have priority as to sending ships.
        /// </summary>
        /// <param name="planetToAttack"></param>
        private bool AttackPlanet(Planet planetToAttack)
        {
            List<Planet> closestPlanets = PW.MyPlanets();
            IComparer<Planet> comparer = new DistanceComparer(planetToAttack, this.Athena);
            closestPlanets.Sort(comparer);

            int shipsToSend = planetToAttack.NumShips + 1;

            foreach (Planet t in closestPlanets)
            {
                if (shipsToSend == 0)
                    break;

                int shipsToSendFromThisPlanet = Math.Min(shipsToSend, this.Athena.Oracle.FreeShipsOnPlanet(t, Player.Me));

                if (shipsToSendFromThisPlanet <= 0) continue;

                this.Athena.IssueOrder(t, planetToAttack, shipsToSendFromThisPlanet);
                shipsToSend -= shipsToSendFromThisPlanet;
            }

            return shipsToSend == 0;
        }

        private void SendAFewShipsToNearestEnemyPlanet()
        {
            foreach (Planet planet in PW.MyPlanets())
            {
                Planet closestEnemyPlanet = FindClosestEnemyPlanet(planet);

                if (closestEnemyPlanet == null)
                    continue;

                int shipsToSend = Math.Min(planet.NumShips / 10, this.Athena.Oracle.FreeShipsOnPlanet(planet, Player.Me));

                if (shipsToSend > 0)
                {
                    this.Athena.IssueOrder(planet, closestEnemyPlanet, shipsToSend);
                }
            }
        }



        private void SendAllShipsToNearestEnemyPlanet()
        {
            foreach (Planet planet in this.PW.MyPlanets())
            {
                // ask defence routine if the planet is available
                Planet closestEnemyPlanet = FindClosestEnemyPlanet(planet);

                if (closestEnemyPlanet == null)
                    continue;

                int numberOfShipsToSend = Math.Min(planet.NumShips, this.Athena.Oracle.FreeShipsOnPlanet(planet, Player.Me));
                if (numberOfShipsToSend > 0)
                {
                    this.Athena.IssueOrder(planet, closestEnemyPlanet, numberOfShipsToSend);
                }

            }
        }

        public Planet FindClosestEnemyPlanet(Planet planet)
        {
            Planet p = null;
            int distance = int.MaxValue;
            foreach (Planet enemyPlanet in this.PW.EnemyPlanets())
            {
                int newDistance = this.Athena.PlanetDistances[planet.Id][enemyPlanet.Id];

                if (newDistance < distance)
                {
                    p = enemyPlanet;
                    distance = newDistance;
                }
            }

            // look at future planets -- only if the planet is further away than the amount of turns before the enemy conquers the planets
            // else we might be attacking neutral planets and all the sudden getting sniped by the enemy force
            foreach (Tuple<Planet, int> futureEnemyPlanet in this.Athena.Oracle.FutureEnemyPlanets())
            {
                int newDistance = this.Athena.PlanetDistances[planet.Id][futureEnemyPlanet.Value1.Id];
                if (futureEnemyPlanet.Value2 < newDistance)
                {
                    if (newDistance < distance)
                    {
                        p = futureEnemyPlanet.Value1;
                        distance = newDistance;
                    }
                }
            }

            return p;
        }

        public Planet FindClosestNeutralPlanet(Planet planet)
        {
            Planet p = null;
            int distance = int.MaxValue;
            foreach (Planet enemyPlanet in this.PW.NeutralPlanets())
            {
                int newDistance = this.Athena.PlanetDistances[planet.Id][enemyPlanet.Id];

                if (newDistance < distance)
                {
                    p = enemyPlanet;
                    distance = newDistance;
                }
            }

            return p;
        }
    }

    internal class GainFromConquestComparer : IComparer<Tuple<int, Planet>>
    {
        public int Compare(Tuple<int, Planet> x, Tuple<int, Planet> y)
        {
            return y.Value1.CompareTo(x.Value1);
        }
    }

    /// <summary>
    /// Class used to sort lists of planets based on the distance to a planet
    /// </summary>
    public class DistanceComparer : IComparer<Planet>
    {
        private readonly Planet planet;
        private readonly Athena athena;

        public DistanceComparer(Planet planet, Athena athena)
        {
            this.planet = planet;
            this.athena = athena;
        }

        public int Compare(Planet x, Planet y)
        {
            int distanceToX = athena.PlanetDistances[x.Id][planet.Id];
            int distanceToY = athena.PlanetDistances[y.Id][planet.Id];

            return distanceToX.CompareTo(distanceToY);
        }
    }
}
