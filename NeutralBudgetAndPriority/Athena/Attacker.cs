using System;
using System.Linq;
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

        //        NEUTRALBUDGET
 
        //= FA - FF + DF * PA
        //FA = vores totale styrker (kun dem, som ikke er allokeret til angreb=tab)
        //FF = fjendens totale styrke (kun dem, som ikke er allokeret til angreb)
        //DF = afstanden til fjenden (de to center of mass)
        //PA = vores totale produktion
        //PF = fjendens totale produktion
 
        //Udvid budget, hvis du kan finde neutrale planeter, hvorom det gælder, at:
        //Det vil sige, neutrale, vi kan tage, genvinde tabet og forstærke hovedplaneten INDEN fjenden kan eksekvere ALLOUT på os.
        //= PN * ( DF - DN * 2 ) - FN
 
        //PRIORITERET LISTE
 
        //Score = [ ( 2,5 * DF - DN ) * PN - FN ] / FN, hvor
        //DF = afstanden til fjenden
        //DN = afstanden til neutrale
        //FN = neutralforsvaret
        //PN = neutralproduktion
 
        //For ethvert givent budget, angrib i rækkefølge.
        //Når budgettet er opbrugt, så overvej at springe i rækkefølgen som følger:
        //Findes der en planet, som vi kan erobre for restbudgettet, i så fald gøre det hvis denne betingelse holder:
        //- genberegn score til version  to, hvor
        //- planeter vi IKKE kan tage inden for budget får forøget DN til når vi faktisk kan overtage dem: DN = DN + ( FN - restbudget) / PA
        //- hvis vi får en topscorer, vi kan overtage, går det, ellers glem det og gå til næste tur, altså opsparing til næste runde.
        private bool OptimizeProductionByAttacking()
        {
            Vector myCom = this.Athena.FindPlayerCenterOfMass(Player.Me);
            IEnumerable<Planet> consideredPlanets = GenerateConsideredPlanet();
               

            // 1 Neutral budget
            int fa = this.Athena.MyFleetCountNotLostToNeutrals() + this.Athena.MyShipCount;
            int ff = this.Athena.EnemyFleetCountNotLostToNeutrals() + this.Athena.EnemyShipCount;
            int df = this.Athena.TimeBetweenPlayersCenterOfMass();
            int pa = this.Athena.MyShipProduction;
            //int pf = this.Athena.EnemyShipProduction;

            int neutralBudget = fa - ff + df * pa;

            // 2 Extended budget
            int extendedBudget = 0;
            foreach (Planet notMyPlanet in consideredPlanets)
            {
                int pn = notMyPlanet.GrowthRate;
                int dn = myCom.DistanceTo(notMyPlanet.Location);
                int fn = notMyPlanet.NumShips;
                int planetExtendedBudget = pn*(df - dn*2) - fn;
  
                if (planetExtendedBudget > 0)
                {
                    extendedBudget += planetExtendedBudget;
                }
            }

            // 3 Priority list
            List<Tuple<double , Planet>> planetScores = CalculateIterationOneScore(df, consideredPlanets, myCom);
            planetScores.Sort(new GainFromConquestComparer<double>());
                
            // 4 attack phase
            int totalBudget = Math.Min(this.Athena.Oracle.TotalFreeShips(Player.Me), neutralBudget + extendedBudget);
            bool tookPlanets = true;
            
            foreach (Tuple<double, Planet> planetScore in planetScores)
            {
                int shipsNeeded = planetScore.Value2.NumShips + 1;
                if (shipsNeeded <= totalBudget)
                {
                    AttackPlanet(planetScore.Value2);
                    totalBudget -= shipsNeeded;
                }
                else
                {
                    tookPlanets = false;
                    break;
                }
            }

            if (tookPlanets)
            {
                // we've taken all the planets we want
                return true;
            }
            
            // sort away planets we've already taken, but include enemy planets
            consideredPlanets = GenerateConsideredPlanet();
            
            planetScores = CalculateIterationTwoScore(df, consideredPlanets, myCom, totalBudget, pa);
            planetScores.Sort(new GainFromConquestComparer<double>());
            tookPlanets = true;
            foreach (Tuple<double, Planet> planetScore in planetScores)
            {
                int shipsNeeded = planetScore.Value2.NumShips + 1;
                if (shipsNeeded <= totalBudget)
                {
                    AttackPlanet(planetScore.Value2);
                    totalBudget -= shipsNeeded;
                }
                else
                {
                    tookPlanets = false;
                    break;
                }
            }

            return tookPlanets;
        }

        private IEnumerable<Planet> GenerateConsideredPlanet()
        {
            //if (this.Athena.Turn == 1 || this.Athena.Turn == 2 || this.Athena.Turn == 3 || this.Athena.Turn == 4)
            {
                return this.PW.NotMyPlanets().Where(
                   p =>
                   this.Athena.PlanetDistances[meStartPlanet.Id][p.Id] <
                   this.Athena.PlanetDistances[enemyStartPlanet.Id][p.Id] && !this.Athena.Oracle.IsPlayerFutureOwner(p, Player.Me));
            }
            return this.PW.NotMyPlanets().Where(p => !this.Athena.Oracle.IsPlayerFutureOwner(p, Player.Me));
        }

        private static List<Tuple<double, Planet>> CalculateIterationTwoScore(int df, IEnumerable<Planet> consideredPlanets, Vector myCom, int restBudget, int pa)
        {
            var result = new List<Tuple<double, Planet>>();
            foreach (Planet notMyPlanet in consideredPlanets)
            {
                int pn = notMyPlanet.GrowthRate;
                int fn = notMyPlanet.NumShips;
                int dn = myCom.DistanceTo(notMyPlanet.Location) + (fn - restBudget) / pa;

                double score;
                if (notMyPlanet.Owner == Player.Enemy)
                {
                    score = (((2.5 * df - dn) * (2*pn)) / fn); // for enemies, we get twice the production, and no loss in forces (since enemy take same loss)
                }
                else
                {
                    score = (((2.5 * df - dn) * pn - fn) / fn);
                }
                result.Add(new Tuple<double, Planet>(score, notMyPlanet));
            }

            return result;
        }

        private static List<Tuple<double, Planet>> CalculateIterationOneScore(int df, IEnumerable<Planet> consideredPlanets, Vector myCom)
        {
            var result = new List<Tuple<double, Planet>>();
            foreach (Planet notMyPlanet in consideredPlanets)
            {
                int pn = notMyPlanet.GrowthRate;
                int dn = myCom.DistanceTo(notMyPlanet.Location);
                int fn = notMyPlanet.NumShips;

                double score;
                if (notMyPlanet.Owner == Player.Enemy)
                {
                    score = (((2.5 * df - dn) * (2 * pn)) / fn); // for enemies, we get twice the production, and no loss in forces (since enemy take same loss)
                }
                else
                {
                    score = (((2.5 * df - dn) * pn - fn) / fn);
                }

                result.Add(new Tuple<double, Planet>(score, notMyPlanet));
            }

            return result;
        }

        /// <summary>
        /// Will send ships from all planets to the planet until it is conquered. The planets closest have priority as to sending ships.
        /// </summary>
        /// <param name="planetToAttack"></param>
        private void AttackPlanet(Planet planetToAttack)
        {
            List<Planet> closestPlanets = PW.MyPlanets();
            IComparer<Planet> comparer = new FurtestDistanceComparer(this.Athena.FindPlayerCenterOfMass(Player.Enemy));
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

            if (shipsToSend > 0)
                throw new InvalidOperationException(" WHAT THE FUCK");
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
                    if (newDistance -2 < distance)
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

    internal class GainFromConquestComparer<T> : IComparer<Tuple<T, Planet>> where T : IComparable
    {
        public int Compare(Tuple<T, Planet> x, Tuple<T, Planet> y)
        {
            return y.Value1.CompareTo(x.Value1);
        }
    }

    /// <summary>
    /// Class used to sort lists of planets based on the distance to a planet
    /// </summary>
    public class ClosestDistanceComparer : IComparer<Planet>
    {
        private readonly Vector v;
        private readonly Planet planet;
        private readonly Athena athena;

        private readonly bool useVector;

        private  ClosestDistanceComparer()
        {
            
        }

        public ClosestDistanceComparer(Planet planet, Athena athena) : this()
        {
            
            this.planet = planet;
            this.athena = athena;
        }

        public ClosestDistanceComparer(Vector v)
            : this()
        {
            this.useVector = true;
            this.v = v;
        }

        public int Compare(Planet x, Planet y)
        {
            int distanceToX;
            int distanceToY;

            if (useVector)
            {
                distanceToX = x.Location.DistanceTo(v);
                distanceToY = y.Location.DistanceTo(v);
            }
            else
            {
                distanceToX = athena.PlanetDistances[x.Id][planet.Id];
                distanceToY = athena.PlanetDistances[y.Id][planet.Id];    
            }

            return distanceToX.CompareTo(distanceToY);
        }
    }

    /// <summary>
    /// Class used to sort lists of planets based on the distance to a planet
    /// </summary>
    public class FurtestDistanceComparer : IComparer<Planet>
    {
        private readonly Vector v;
        private readonly Planet planet;
        private readonly Athena athena;

        private readonly bool useVector;

        private FurtestDistanceComparer()
        {

        }

        public FurtestDistanceComparer(Planet planet, Athena athena)
            : this()
        {

            this.planet = planet;
            this.athena = athena;
        }

        public FurtestDistanceComparer(Vector v)
            : this()
        {
            this.useVector = true;
            this.v = v;
        }

        public int Compare(Planet x, Planet y)
        {
            int distanceToX;
            int distanceToY;

            if (useVector)
            {
                distanceToX = x.Location.DistanceTo(v);
                distanceToY = y.Location.DistanceTo(v);
            }
            else
            {
                distanceToX = athena.PlanetDistances[x.Id][planet.Id];
                distanceToY = athena.PlanetDistances[y.Id][planet.Id];
            }

            return distanceToY.CompareTo(distanceToX);
        }
    }
}
