using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AthenaBot
{
    public class Athena
    {
        // Bot parameters
        private const bool IncludeFleetsNotOnPlanetsInCenterOfMassComputation = true;

        public Athena()
        {
            this.Turn = 1;
            this.PlanetDistances = new Dictionary<int, Dictionary<int, int>>();
        }

        /// <summary>
        /// Entry point for each turn.
        /// </summary>
        /// <param name="planetWars">The current model of the game</param>
        public void DoTurn(PlanetWars planetWars)
        {
#if LOCAL
            Stopwatch sw = new Stopwatch();
            sw.Start();

#endif
            Initialize(planetWars);

            Oracle.Foretell();
            Defender.Defend();
            Router.Route();
            Attacker.Attack();

            Turn++;
#if LOCAL

            File.AppendAllText("lol.log" , Environment.NewLine + sw.ElapsedMilliseconds);
#endif
        }

        private void Initialize(PlanetWars planetWars)
        {
            this.PW = planetWars;

            if (Turn == 1)
            {
                AnalyzeMap();
            }

            if (Oracle == null)
            {
                Oracle = new Oracle(PW, this);
            }

            if (Defender == null)
            {
                Defender = new Defender(PW, this);
            }

            if (Attacker == null)
            {
                Attacker = new Attacker(PW, this);
            }

            if (Router == null)
            {
                Router = new Router(PW, this);
            }

            this.Oracle.PlanetWars = PW;
            this.Defender.PlanetWars = PW;
            this.Attacker.PW = PW;
            this.Router.PW = PW;
        }

        public int TimeBetweenPlayersCenterOfMass()
        {
            Vector myCenterOfMass = FindPlayerCenterOfMass(PW.MyFleets(), PW.MyPlanets(), Athena.IncludeFleetsNotOnPlanetsInCenterOfMassComputation);
            Vector enemyCenterOfMass = FindPlayerCenterOfMass(PW.EnemyFleets(), PW.EnemyPlanets(), Athena.IncludeFleetsNotOnPlanetsInCenterOfMassComputation);

            return myCenterOfMass.DistanceTo(enemyCenterOfMass);
        }


        public Vector FindPlayerCenterOfMass(Player player)
        {
            switch (player)
            {
                case Player.Me:
                    return FindPlayerCenterOfMass(PW.MyFleets(), PW.MyPlanets(), Athena.IncludeFleetsNotOnPlanetsInCenterOfMassComputation);
                case Player.Enemy:
                    return FindPlayerCenterOfMass(PW.EnemyFleets(), PW.EnemyPlanets(), Athena.IncludeFleetsNotOnPlanetsInCenterOfMassComputation);
                default:
                    throw new ArgumentOutOfRangeException("player");
            }
        }

        /// <summary>
        /// har jeg fx 2 skibe i (100,100) og 1 i (0,200), så vil det være (50% af (2*100+0),50% af 2*100+200)=(100,133) )
        /// </summary>
        /// <param name="fleets"></param>
        /// <param name="planets"></param>
        /// <param name="weightFleets"></param>
        /// <returns></returns>
        public Vector FindPlayerCenterOfMass(IEnumerable<Fleet> fleets, IEnumerable<Planet> planets, bool weightFleets)
        {
            float x = 0;
            float y = 0;

            int totalNumberOfShips = 0;

            foreach (Planet item in planets)
            {
                x += (float)(item.Location.X * item.NumShips);
                y += (float)(item.Location.Y * item.NumShips);

                totalNumberOfShips += item.NumShips;
            }

            if (weightFleets)
            {
                foreach (Fleet fleet in fleets)
                {
                    x += (float)this.PW.GetPlanet(fleet.DestinationPlanet).Location.X * fleet.NumShips;
                    y += (float)this.PW.GetPlanet(fleet.DestinationPlanet).Location.Y * fleet.NumShips;

                    totalNumberOfShips += fleet.NumShips;
                }
            }

            return new Vector(x / totalNumberOfShips, y / totalNumberOfShips);
        }

        /// <summary>
        /// Analyzes and stores various properties of the map such as distances between planets and planet growth rates.
        /// </summary>
        private void AnalyzeMap()
        {
            MaxDistanceBetweenPlanets = int.MinValue;
            // setup distance table between planets
            foreach (Planet planet in PW.Planets)
            {
                Vector v1 = planet.Location;
                foreach (Planet planet2 in PW.Planets)
                {
                    int distance = v1.DistanceTo(planet2.Location);

                    if (!this.PlanetDistances.ContainsKey(planet.Id))
                    {
                        PlanetDistances[planet.Id] = new Dictionary<int, int>();
                    }

                    PlanetDistances[planet.Id].Add(planet2.Id, distance);

                    MaxDistanceBetweenPlanets = Math.Max(MaxDistanceBetweenPlanets, distance);
                }

                // setup list of planets sorted by their growth rate
                //growthRateSortedList.Add(planet.Id, planet.GrowthRate);
            }
        }


        public void IssueOrder(Planet source, Planet destination, int ships)
        {
            PlanetWars.IssueOrder(source, destination, ships);

            int distanceBetweenPlanets = source.Location.DistanceTo(destination.Location);
            this.Oracle.ShipCount[destination.Id][distanceBetweenPlanets].AddShips(ships, Player.Me);
            this.Oracle.ShipCount[source.Id][0].AddShips(-ships, Player.Me);

            this.Oracle.ResolutePlanet(destination, distanceBetweenPlanets);
            this.Oracle.ResolutePlanet(source, 0);
        }

        #region "Properties"
        public PlanetWars PW { get; set; }
        public Defender Defender { get; set; }
        public Attacker Attacker { get; set; }
        public Router Router { get; set; }
        public int Turn { get; set; }
        public Dictionary<int, Dictionary<int, int>> PlanetDistances { get; private set; }
        public Oracle Oracle { get; set; }
        public int MaxDistanceBetweenPlanets { get; set; }

        public float ShipRatio
        {
            get
            {
                return (float)MyShipCount / EnemyShipCount;
            }
        }

        public float ProductionRatio
        {
            get
            {
                return (float)MyShipProduction / EnemyShipProduction;
            }
        }

        public int MyShipCount
        {
            get { return PW.MyPlanets().Sum(p => p.NumShips); }
        }

        public int MyFleetCountNotLostToNeutrals()
        {
            int fleetsHeadingTowardsAllyOrEnemy = PW.MyFleets().Where(fleet => PW.Planets[fleet.DestinationPlanet].Owner != Player.Neutral).Sum(fleet => fleet.NumShips);

            int survivingFleetsHeadingTowardsNeutral =PW.MyFleets().Where(fleet => PW.Planets[fleet.DestinationPlanet].Owner == Player.Neutral).
                                Sum(fleet => Math.Max(fleet.NumShips - PW.Planets[fleet.DestinationPlanet].NumShips,0));

            return fleetsHeadingTowardsAllyOrEnemy + survivingFleetsHeadingTowardsNeutral;
                
                
        }

        public int EnemyFleetCountNotLostToNeutrals()
        {
            return PW.EnemyFleets().Where(f => PW.Planets[f.DestinationPlanet].Owner != Player.Neutral).Sum(f => f.NumShips)
                + PW.EnemyFleets().Where(f => PW.Planets[f.DestinationPlanet].Owner == Player.Neutral).Sum(f => Math.Max(f.NumShips - PW.Planets[f.DestinationPlanet].NumShips, 0));
        }

        public int MyShipProduction
        {
            get { return PW.MyPlanets().Sum(p => p.GrowthRate); }
        }

        public int EnemyShipCount
        {
            get { return PW.EnemyPlanets().Sum(p => p.NumShips); }
        }

        public int EnemyShipProduction
        {
            get { return PW.EnemyPlanets().Sum(p => p.GrowthRate); }
        }



        #endregion

    }
}
