using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema4
{
    internal class Edge: IComparable<Edge>
    {
        public double Distance;
        public int From, To;
        public Edge(double distance,int from, int to)
        {
            Distance = distance;
            From = from;
            To = to;
        }

        public int CompareTo(Edge? other)
        {
            return this.Distance.CompareTo(other.Distance);
        }
    }
}
