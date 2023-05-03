using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problema4
{
    internal class Point3D
    {
        static Random rnd = new Random();
        public int X, Y, Z;

        public Point3D(int x,int y,int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double DistanceE(Point3D otherPoint)
        {
            double x = this.X - otherPoint.X;
            double y = this.Y - otherPoint.Y;
            double z = this.Z - otherPoint.Z;

            return Math.Sqrt(x*x + y*y + z*z);
        }

        public static Point3D[] GenerateRandomPoints3D(int n,int MaxX,int MaxY, int MaxZ)
        {
            Point3D[] points = new Point3D[n];
            for (int i = 0; i < n; i++)
            {
                int x = rnd.Next(MaxX);
                int y = rnd.Next(MaxY);
                int z = rnd.Next(MaxZ);
                points[i] = new Point3D(x, y, z);
            }
            return points;
        }
    }
}
