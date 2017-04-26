namespace PlanetWarsBot
{
    public struct DefenceContract
    {
        private Planet planet;

        public Planet Planet
        {
            get { return planet; }
            set { planet = value; }
        }


        private int shipsNeeded;

        public int ShipsNeeded
        {
            get { return shipsNeeded; }
            set { shipsNeeded = value; }
        }
        

        private int timeFrame;

        public int TimeFrame
        {
            get { return timeFrame; }
            set { timeFrame = value; }
        }


        public DefenceContract(int shipsNeeded, int timeFrame, Planet p)
        {
            this.planet = p;
            this.timeFrame = timeFrame;
            this.shipsNeeded = shipsNeeded;
        }

        public override string ToString()
        {
            return string.Format("[{3}]t {0} s {1} men {2}", timeFrame, shipsNeeded, planet.NumShips(), QwertyBot.TurnNumber);
        }
    }
}
