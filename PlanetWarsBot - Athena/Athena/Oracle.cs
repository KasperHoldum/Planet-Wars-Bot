using System;
using System.Collections.Generic;
using System.Linq;

namespace AthenaBot
{
    public class Oracle
    {
        public struct PlanetState
        {
            public Player Owner;
            public int NumShips;

            private readonly Dictionary<Player, int> numberOfShips;

            public PlanetState(Player owner)
            {
                numberOfShips = new Dictionary<Player, int>();
                NumShips = 0;
                this.Owner = owner;

                ResetIncomingShips();
            }

            private void ResetIncomingShips()
            {
                numberOfShips[Player.Neutral] = 0;
                numberOfShips[Player.Me] = 0;
                numberOfShips[Player.Enemy] = 0;
            }

            public void AddShips(int numShips, Player owner)
            {
                numberOfShips[owner] += numShips;
            }

            public void DoBattleResolution()
            {
                int shipsLeft;
                Player shipsOwner = this.Owner;

                numberOfShips[Owner] += this.NumShips;

                int enemyShips = numberOfShips[Player.Enemy];
                int myShips = numberOfShips[Player.Me];
                int neutralShips = numberOfShips[Player.Neutral];

                if (enemyShips > 0 && myShips > 0 && neutralShips > 0) // 3 force resolution
                {
                    int higestCount = enemyShips;
                    Player highestCountOwner = Player.Enemy;
                    int secondHighest;

                    // determine highest count, highest count owner and second highest count
                    if (myShips > higestCount)
                    {
                        secondHighest = higestCount;
                        highestCountOwner = Player.Me;
                        higestCount = myShips;
                    }
                    else
                    {
                        secondHighest = myShips;
                    }

                    if (neutralShips > higestCount)
                    {
                        secondHighest = higestCount;
                        highestCountOwner = Player.Neutral;
                        higestCount = neutralShips;
                    }
                    else if (neutralShips > secondHighest)
                    {
                        secondHighest = neutralShips;
                    }

                    
                    //
                    if (higestCount == secondHighest)
                    {
                        shipsLeft = 0;
                    }
                    else
                    {
                        shipsLeft = higestCount - secondHighest;
                        shipsOwner = highestCountOwner;
                    }
                }
                else if (enemyShips > 0 && neutralShips > 0)
                {
                    shipsLeft = Math.Abs(enemyShips - neutralShips);
                    shipsOwner = enemyShips > neutralShips ? Player.Enemy : (enemyShips == neutralShips ? this.Owner : Player.Neutral);
                }
                else if (myShips >0  && neutralShips > 0)
                {
                    shipsLeft = Math.Abs(myShips - neutralShips);
                    shipsOwner = myShips > neutralShips ? Player.Me : (myShips == neutralShips ? this.Owner : Player.Neutral);
                }
                else if (myShips >0 && enemyShips>0)
                {
                    shipsLeft = Math.Abs(myShips - enemyShips);
                    shipsOwner = myShips > enemyShips ? Player.Me : (myShips == enemyShips ? this.Owner : Player.Enemy);
                }
                else
                {
                    shipsLeft = numberOfShips[this.Owner];
                }

                this.Owner = shipsOwner;
                this.NumShips = shipsLeft;
                ResetIncomingShips();
            }
        }

        public int TurnsToLookIntoFuture { get; private set; }
        public PlanetWars PlanetWars { get; set; }
        public Athena Athena { get; set; }
        public PlanetState[][] ShipCount { get; private set; }

        public Oracle(PlanetWars pw, Athena athena)
        {
            this.PlanetWars = pw;
            this.Athena = athena;

            this.TurnsToLookIntoFuture = athena.MaxDistanceBetweenPlanets+1;
            
            // Initialize matrix
            ShipCount = new PlanetState[pw.NumPlanets][];
            for (int i = 0; i < ShipCount.GetLength(0); i++)
            {
                ShipCount[i] = new PlanetState[TurnsToLookIntoFuture];
            }
        }

        /// <summary>
        /// Oracle entry-point
        /// </summary>
        public void Foretell()
        {
            // 1. Calculate the amounts of ships on each planet at any given turn
            PredictShipsAtEachPlanet();
        }

        /// <summary>
        /// Predicts the state of all planets in a specific amount of turns into the future.
        /// </summary>
        private void PredictShipsAtEachPlanet()
        {
            for (int planetId = 0; planetId < this.PlanetWars.NumPlanets; planetId++)
            {
                for (int turn = 0; turn < TurnsToLookIntoFuture; turn++)
                {
                    ShipCount[planetId][turn] = ComputePlanetState(turn, this.PlanetWars.Planets[planetId]);
                }
            }
        }

        private PlanetState ComputePlanetState(int turns, Planet p)
        {
            PlanetState state = new PlanetState(p.Owner);

            // we can't use any previous prediction
            if (turns == 0)
            {
                state.Owner = p.Owner;
                state.NumShips = p.NumShips;
            }
            else // use last turns predication as seed
            {
                state.Owner = ShipCount[p.Id][turns-1].Owner; // owner is still the same as last round
                state.NumShips = ShipCount[p.Id][turns - 1].NumShips;
                if (state.Owner != Player.Neutral)
                    state.NumShips += p.GrowthRate;
            }
            // 1. look at all incoming ships
            foreach (Fleet fleet in this.PlanetWars.Fleets())
            {
                if (fleet.DestinationPlanet != p.Id) continue; // only interested in fleets heading to this planet

                // 2. see if any of them will hit in this turn
                if (fleet.TurnsRemaining == turns)
                {
                    state.AddShips(fleet.NumShips, fleet.Owner);
                }
            }
            state.DoBattleResolution();

            return state;
        }

        public void ResolutePlanet(Planet item, int startTurn)
        {
            ShipCount[item.Id][startTurn].DoBattleResolution();

            for (int turn = startTurn+1; turn < TurnsToLookIntoFuture; turn++) // possible overflow
            {
                ShipCount[item.Id][turn] = ComputePlanetState(turn, item);
            }

        }

        public int FreeShipsOnPlanet(Planet p, Player player)
        {
            int minAmountOfShips = int.MaxValue;

            for (int turn = 0; turn < this.TurnsToLookIntoFuture; turn++)
            {
                if (this.ShipCount[p.Id][ turn].Owner == player)
                {
                    minAmountOfShips = Math.Min(this.ShipCount[p.Id][turn].NumShips, minAmountOfShips);
                }
                else
                {
                    return 0;
                }
            }

            return minAmountOfShips;
        }

        public int TotalFreeShips(Player p)
        {
            switch (p)
            {
                case Player.Me:
                    return PlanetWars.MyPlanets().Sum(planet => FreeShipsOnPlanet(planet, p));
                case Player.Enemy:
                    return PlanetWars.EnemyPlanets().Sum(planet => FreeShipsOnPlanet(planet, p));
                case Player.Neutral:
                    return PlanetWars.NeutralPlanets().Sum(planet => FreeShipsOnPlanet(planet, p));
                default:
                    throw new ArgumentOutOfRangeException("p");
            }

        }

        public bool IsPlayerFutureOwner(Planet p, Player player)
        {
            return ShipCount[p.Id][TurnsToLookIntoFuture-1].Owner == player;
        }


        /// <summary>
        /// Returns a tuple of planets that the enemy will conquer alongside the turn in which he will conquer it
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tuple<Planet, int>> FutureEnemyPlanets()
        {
            IList<Tuple<Planet, int>> results = new List<Tuple<Planet, int>>();

            for (int planetId = 0; planetId < this.PlanetWars.NumPlanets; planetId++)
            {
                if (this.PlanetWars.Planets[planetId].Owner == Player.Enemy)
                    continue;

                for (int turn = 1; turn < this.TurnsToLookIntoFuture; turn++)
                {
                    if (this.ShipCount[planetId][turn].Owner == Player.Enemy)
                    {
                        results.Add(new Tuple<Planet, int>(this.PlanetWars.Planets[planetId], turn));
                    }
                }        
            }
            
            return results;
        }

        public IEnumerable<Tuple<Planet, int>> FutureMePlanets()
        {
            IList<Tuple<Planet, int>> results = new List<Tuple<Planet, int>>();

            for (int planetId = 0; planetId < this.PlanetWars.NumPlanets; planetId++)
            {
                if (this.PlanetWars.Planets[planetId].Owner == Player.Me)
                    continue;

                for (int turn = 1; turn < this.TurnsToLookIntoFuture; turn++)
                {
                    if (this.ShipCount[planetId][turn].Owner == Player.Me)
                    {
                        results.Add(new Tuple<Planet, int>(this.PlanetWars.Planets[planetId], turn));
                    }
                }
            }

            return results; 
        }

        public IEnumerable<Planet> NeutralMeEnemyPlanets()
        {
            IList<Planet> results = new List<Planet>();

            bool hasBeenOwnedByMe = false;
            bool isLastOwnedByEnemy = false;

            for (int planetId = 0; planetId < this.PlanetWars.NumPlanets; planetId++)
            {
                if (this.PlanetWars.Planets[planetId].Owner != Player.Neutral)
                    continue;

                for (int turn = 1; turn < this.TurnsToLookIntoFuture; turn++)
                {
                    Player turnOwner = this.ShipCount[planetId][turn].Owner;
                        if (turnOwner == Player.Me)
                        {
                            hasBeenOwnedByMe = true;
                        }

                    isLastOwnedByEnemy = turnOwner == Player.Enemy;
                }

                if (hasBeenOwnedByMe && isLastOwnedByEnemy)
                {
                    results.Add(this.PlanetWars.Planets[planetId]);
                }
            }

            return results; 
        }
    }
}
