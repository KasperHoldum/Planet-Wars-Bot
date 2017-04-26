using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWarsBot
{
    public class Vector
    {
        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }


        public Vector()
        {

        }

        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public int DistanceTo(Vector v2)
        {
            return Distance(this, v2);
        }

        public static int Distance(Vector v1, Vector v2)
        {
            double dx = v1.X - v2.X;
            double dy = v1.Y - v2.Y;
            return (int)Math.Ceiling(Math.Sqrt(dx * dx + dy * dy));
        }

        public override string ToString()
        {
            return string.Format("({0};{1})", x, y);
        }

    }
}
