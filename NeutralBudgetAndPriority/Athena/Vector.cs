using System;
using System.Threading;

namespace AthenaBot
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

        public int DistanceTo(Vector vector)
        {
            return Distance(this, vector);
        }

        public static int Distance(Vector vector1, Vector vector2)
        {
            double dx = vector1.X - vector2.X;
            double dy = vector1.Y - vector2.Y;
            return (int)Math.Ceiling(Math.Sqrt(dx * dx + dy * dy));
        }

        public override string ToString()
        {
            return string.Format("({0};{1})", x, y);
        }

    }
}
