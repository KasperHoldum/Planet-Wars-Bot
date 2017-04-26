using System;

namespace AthenaBot
{

    public class Planet
    {
        // Initializes a planet.
        public Planet(int planetId,
                      int owner,
              int numShips,
              int growthRate,
              double x,
              double y)
        {
            this.Id = planetId;
            this.Owner = (Player)owner;
            this.NumShips = numShips;
            this.GrowthRate = growthRate;
            this.location = new Vector(x, y);
        }

        private Vector location;

        public Vector Location
        {
            get { return location; }
        }

        public void AddShips(int amount)
        {
            NumShips += amount;
        }

        public void RemoveShips(int amount)
        {
            NumShips -= amount;
        }

        private int id;
        public int Id
        {
            get { return id; }
            private set { id = value; }
        }

        private Player owner;
        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private int numShips;
        public int NumShips
        {
            get { return numShips; }
            set { numShips = value; }
        }

        private int growthRate;
        public int GrowthRate
        {
            get { return growthRate; }
            private set { growthRate = value; }
        }

        public override string ToString()
        {
            return "Planet " + Id;
        }
    }
}