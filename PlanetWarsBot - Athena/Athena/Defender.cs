using System;
using System.Collections.Generic;
using System.Linq;
namespace AthenaBot
{
    public class Defender
    {
        public Defender(PlanetWars pw, Athena athena)
        {
            this.Athena = athena;
            this.PlanetWars = pw;
        }

        public void Defend()
        {
            // 3 send assistance to future owned planets that we will lose
            SendAssistanceToFutureLostPlanets();
            // 2. send assistance to any planet that goes negative
            SendAssistanceToLostPlanets();

         
        }

        private void SendAssistanceToFutureLostPlanets()
        {
            // loop through all planets to see if they need help
            foreach (Planet item in this.Athena.Oracle.NeutralMeEnemyPlanets())
            {
                // loop throuhg future turns to see if enemy will capture this planet
                for (int turn = 0; turn < this.Athena.Oracle.TurnsToLookIntoFuture; turn++)
                {
                    // There are ships on this planet equal to the prediction + any support sent in previous turns
                    var planetState = this.Athena.Oracle.ShipCount[item.Id][turn];

                    // we've been dominated - ask for assistance
                    if (planetState.Owner != Player.Me)
                    {
                        // send assistance 
                        int assistanceNeededThisTurn = planetState.NumShips;
                        bool succesfullyGotAssistance = SendDefenceToPlanet(item, assistanceNeededThisTurn, turn);

                        // if no help, skip to next planet.
                        if (!succesfullyGotAssistance)
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Looks at every planet and it's future, and tries to assist planets that will get captured.
        /// </summary>
        private void SendAssistanceToLostPlanets()
        {
            // loop through all planets to see if they need help
            foreach (Planet item in PlanetWars.MyPlanets())
            {
                // loop throuhg future turns to see if enemy will capture this planet
                for (int turn = 0; turn < this.Athena.Oracle.TurnsToLookIntoFuture; turn++)
                {
                    // There are ships on this planet equal to the prediction + any support sent in previous turns
                    var planetState = this.Athena.Oracle.ShipCount[item.Id][turn];

                    // we've been dominated - ask for assistance
                    if (planetState.Owner != Player.Me)
                    {
                        // send assistance 
                        int assistanceNeededThisTurn = planetState.NumShips;
                        bool succesfullyGotAssistance = SendDefenceToPlanet(item, assistanceNeededThisTurn, turn);

                        // if no help, skip to next planet.
                        if (!succesfullyGotAssistance)
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A planet is about to get dominated. Search for help in aroundlying planets
        /// </summary>
        /// <param name="planetToDefend"></param>
        /// <param name="shipsNeeded"></param>
        /// <param name="turnsToArrive"></param>
        /// <returns></returns>
        private bool SendDefenceToPlanet(Planet planetToDefend, int shipsNeeded, int turnsToArrive)
        {
            List<Planet> closestPlanets = this.PlanetWars.MyPlanets();
            IComparer<Planet> comparer = new DistanceComparer(planetToDefend, this.Athena);
            closestPlanets.Sort(comparer);

            int shipsToSend = shipsNeeded;

            List<Tuple<Planet, int>> contributionsFromPlanets = new List<Tuple<Planet, int>>();

            for (int i = 0; i < closestPlanets.Count; i++)
            {
                if (shipsToSend == 0) // we already sent enough ships
                    break;

                if (closestPlanets[i].Id == planetToDefend.Id) // this is the planet under attack
                    continue;

                if (closestPlanets[i].Location.DistanceTo(planetToDefend.Location) > turnsToArrive)
                    break;

                int shipsToSendFromThisPlanet = Math.Min(Math.Min(shipsToSend, closestPlanets[i].NumShips), this.Athena.Oracle.FreeShipsOnPlanet(closestPlanets[i], Player.Me));

                if (shipsToSendFromThisPlanet > 0)
                {
                    contributionsFromPlanets.Add(new Tuple<Planet, int>(closestPlanets[i], shipsToSendFromThisPlanet));
                    shipsToSend -= shipsToSendFromThisPlanet;
                }
            }


            if (shipsToSend == 0)
            {
                foreach (Tuple<Planet, int> attack in contributionsFromPlanets)
                {
                    if (attack.Value2 <= 0) continue;
                    this.Athena.IssueOrder(attack.Value1, planetToDefend, attack.Value2);
                }
            }
            return shipsToSend == 0;
        }



        #region "Properties"
        public PlanetWars PlanetWars { get; set; }
        public Athena Athena { get; set; }
        #endregion
    }
}
