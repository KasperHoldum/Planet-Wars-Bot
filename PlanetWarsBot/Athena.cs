using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlanetWarsBot
{
    public class Athena
    {
        private PlanetWars pw;

        private int turn = 1;

        private int myShipCount;
        private int myShipProduction;

        private int enemyShipCount;
        private int enemyShipProduction;

        private float ShipRatio
        {
            get
            {
                return myShipCount / enemyShipCount;
            }
        }

        private float ProductionRatio
        {
            get
            {
                return myShipProduction / enemyShipProduction;
            }
        }

        private int TimeBetweenPlayersCenterOfMass()
        {
            Vector myCenterOfMass = FindPlayerCenterOfMass(pw.MyFleets(), pw.MyPlanets());
            Vector enemyCenterOfMass = FindPlayerCenterOfMass(pw.EnemyFleets(), pw.EnemyPlanets());

            return myCenterOfMass.DistanceTo(enemyCenterOfMass);
        }

        /// <summary>
        /// har jeg fx 2 skibe i (100,100) og 1 i (0,200), så vil det være (50% af (2*100+0),50% af 2*100+200)=(100,133) )
        /// </summary>
        /// <param name="fleets"></param>
        /// <param name="planets"></param>
        /// <returns></returns>
        private static Vector FindPlayerCenterOfMass(List<Fleet> fleets, IEnumerable<Planet> planets)
        {
            float x = 0;
            float y = 0;

            int totalNumberOfShips = 0;

            foreach (Planet item in planets)
            {
                x += (float)(item.X() * item.NumShips());
                y += (float)(item.Y() * item.NumShips());

                totalNumberOfShips += item.NumShips();
            }

            return new Vector(x / totalNumberOfShips,y /  totalNumberOfShips);
        }

        public void DoTurn(PlanetWars planetWars)
        {
            File.AppendAllText(@"c:\lol.txt", "Turn: " + turn + Environment.NewLine);

            this.pw = planetWars;
            SetupVariables(pw);

            int timeBetweenCenterOfMass = TimeBetweenPlayersCenterOfMass();

            if (myShipCount > enemyShipCount + enemyShipProduction * timeBetweenCenterOfMass) // go all out scenario
            {
                File.AppendAllText(@"c:\lol.txt", "go all out scenario\n" + Environment.NewLine);

                SendAllShipsToNearestEnemyPlanet();
            }
            else if (myShipCount > enemyShipCount & myShipProduction > enemyShipProduction) // trade 1:1 scenario
            {
                File.AppendAllText(@"c:\lol.txt", "// trade 1:1 scenario\n" + Environment.NewLine);
                SendAFewShipsToNearestEnemyPlanet();
            }
            else // start looking for attractive neutral planets closeby
            {
                File.AppendAllText(@"c:\lol.txt", "// start looking for attractive neutral planets closeby\n" + Environment.NewLine);
                Planet[] suitableNeutralPlanets = FindNeutralPlanetsToAttack();

                if (suitableNeutralPlanets.Length > 0)
                {
                    File.AppendAllText(@"c:\lol.txt", "Found neutral planets\n" + Environment.NewLine);
                    // temp code

                    if (suitableNeutralPlanets[0].NumShips() < suitableNeutralPlanets[1].NumShips())
                    {
                        File.AppendAllText(@"c:\lol.txt", "attacked neutral planets\n" + Environment.NewLine);
                        pw.IssueOrder(suitableNeutralPlanets[1], suitableNeutralPlanets[0], suitableNeutralPlanets[1].NumShips() - 1);
                        suitableNeutralPlanets[1].NumShips(1);
                    }
                    //foreach (Planet item in suitableNeutralPlanets)
                    //{
                    //    AttackPlanet(item);
                    //}
                }
                else if (myShipProduction < enemyShipProduction) // PANIC: we're fucked, desperate times require desperate measures
                {
                    File.AppendAllText(@"c:\lol.txt", "we're screwed! PANIC!\n" + Environment.NewLine);

                }

            }
            turn++;

            File.AppendAllText(@"c:\lol.txt", "\n" + Environment.NewLine);
        }

        private void SetupVariables(PlanetWars pw)
        {
            this.enemyShipCount = pw.NumShips(2);
            this.myShipCount = pw.NumShips(1);

            foreach (Planet item in pw.EnemyPlanets())
	{
                this.enemyShipProduction = item.GrowthRate();
	}

            foreach (Planet item in pw.MyPlanets())
            {
                this.myShipProduction = item.GrowthRate();
            }
        }

        private void SendAFewShipsToNearestEnemyPlanet()
        {
            foreach (Planet planet in pw.MyPlanets())
            {
                Planet closestEnemyPlanet = FindClosestEnemyPlanet(planet);
                int shipsToSend = planet.NumShips() /10;
                pw.IssueOrder(planet, closestEnemyPlanet, shipsToSend);
                planet.NumShips( planet.NumShips() - shipsToSend);
            }
        }

        private void AttackPlanet(Planet item)
        {
            throw new NotImplementedException();
        }

        private void SendAllShipsToNearestEnemyPlanet()
        {
            foreach (Planet planet in pw.MyPlanets())
            {
                Planet closestEnemyPlanet = FindClosestEnemyPlanet(planet);

                pw.IssueOrder(planet, closestEnemyPlanet, planet.NumShips() - 1);
                planet.NumShips(1);

            }
        }

        private Planet FindClosestEnemyPlanet(Planet planet)
        {
            Planet p = null;
            float distance = float.MaxValue;
            Vector v = planet.GetVector();
            foreach (Planet enemyPlanet in pw.EnemyPlanets())
            {
                float newDistance = v.DistanceTo(enemyPlanet.GetVector());

                if (newDistance < distance)
                {
                    p = enemyPlanet;
                    distance = newDistance;
                }
            }

#if LOCAL
            if (p == null)
            throw new InvalidOperationException("there should always be a planet closer than infinity");
#endif 


            return p ?? pw.EnemyPlanets()[0];
        }

        private Planet FindClosestNeutralPlanet(Planet planet)
        {
            Planet p = null;
            float distance = float.MaxValue;
            Vector v = planet.GetVector();
            foreach (Planet enemyPlanet in pw.NeutralPlanets())
            {
                float newDistance = v.DistanceTo(enemyPlanet.GetVector());

                if (newDistance < distance)
                {
                    p = enemyPlanet;
                    distance = newDistance;
                }
            }

#if LOCAL
            if (p == null)
            throw new InvalidOperationException("there should always be a planet closer than infinity");
#endif


            return p ?? pw.EnemyPlanets()[0];
        }


        private Planet[] FindNeutralPlanetsToAttack()
        {
            Planet[] planetsToAttack = new Planet[2];

            int distance = int.MaxValue;
            Planet myPlanet = null;
            Planet neutralPlanet = null;

            foreach (Planet planet in pw.MyPlanets())
            {
                Planet closestEnemyPlanet = FindClosestNeutralPlanet(planet);


                if (planet.GetVector().DistanceTo(closestEnemyPlanet.GetVector()) < distance && closestEnemyPlanet.NumShips() < 0.5 * TimeBetweenPlayersCenterOfMass())
                {
                    distance = planet.GetVector().DistanceTo(closestEnemyPlanet.GetVector());
                    myPlanet = planet;
                    neutralPlanet = closestEnemyPlanet;
                }
            }


            planetsToAttack[0] = neutralPlanet;
            planetsToAttack[1] = myPlanet;

            return planetsToAttack;
        }
    }
}
