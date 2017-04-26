namespace AthenaBot
{
    public class Fleet
    {
        // Initializes a fleet.
        public Fleet(int owner,
                     int numShips,
                     int sourcePlanet,
                     int destinationPlanet,
                     int totalTripLength,
                     int turnsRemaining)
        {
            this.Owner = (Player)owner;
            this.NumShips = numShips;
            this.SourcePlanet = sourcePlanet;
            this.DestinationPlanet = destinationPlanet;
            this.TotalTripLength = totalTripLength;
            this.TurnsRemaining = turnsRemaining;
        }

        // Initializes a fleet.
        public Fleet(int owner,
                     int numShips)
        {
            this.Owner = (Player)owner;
            this.NumShips = numShips;
            this.SourcePlanet = -1;
            this.DestinationPlanet = -1;
            this.TotalTripLength = -1;
            this.TurnsRemaining = -1;
        }

        // Accessors and simple modification functions. These should be mostly
        // self-explanatory.

        public void RemoveShips(int amount)
        {
            NumShips -= amount;
        }

        // Subtracts one turn remaining. Call this function to make the fleet get
        // one turn closer to its destination.
        public void TimeStep()
        {
            if (TurnsRemaining > 0)
            {
                --TurnsRemaining;
            }
            else
            {
                TurnsRemaining = 0;
            }
        }

        private Player owner;
        public Player Owner
        {
            get { return owner; }
            private set { owner = value; }
        }

        private int numShips;
        public int NumShips
        {
            get { return numShips; }
            set { numShips = value; }
        }

        private int sourcePlanet;
        public int SourcePlanet
        {
            get { return sourcePlanet; }
            private set { sourcePlanet = value; }
        }

        private int destinationPlanet;
        public int DestinationPlanet
        {
            get { return destinationPlanet; }
            private set { destinationPlanet = value; }
        }

        private int totalTripLength;
        public int TotalTripLength
        {
            get { return totalTripLength; }
            private set { totalTripLength = value; }
        }

        private int turnsRemaining;
        public int TurnsRemaining
        {
            get { return turnsRemaining; }
            set { turnsRemaining = value; }
        }
    }
}
