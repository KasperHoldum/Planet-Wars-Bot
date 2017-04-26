using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWarsBot
{
    public class FleetDistanceComparer : IComparer<Fleet>
    {
        #region IComparer<Fleet> Members

        public int Compare(Fleet x, Fleet y)
        {
            return x.TurnsRemaining() != y.TurnsRemaining() ? x.TurnsRemaining().CompareTo(y.TurnsRemaining()) : 0;
        }

        #endregion
    }
}
