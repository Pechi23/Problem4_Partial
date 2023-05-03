using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;

namespace Problema4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics grp;
        Bitmap bmp;

        //these values can be modified
        int MAX_Z = 100;
        public int n = 30;

        private void solve_Click(object sender, EventArgs e)
        {
            Point3D[] points = Point3D.GenerateRandomPoints3D(n,pictureBox1.Width,pictureBox1.Height,MAX_Z);

            //using Kruskal algorithm for finding the MST, an alternative is Prim algorithm
            List<Edge> resultEdges = Kruskal(points);
            
            //to obtain 3 minimum spanning trees, sort the MST edges and remove the longest 2 of them
            resultEdges.Sort();
            resultEdges.RemoveAt(resultEdges.Count - 1);
            resultEdges.RemoveAt(resultEdges.Count - 1);

            //Drawing the result
            DrawPointsAndEdges2D(points,resultEdges);
        }


        /// <summary>
        /// Kruskal algorithm that finds the minimun spanning tree (MST) and runs in O(n^2). 
        /// An aproach that runs in O(nlogn) can be found here:
        /// https://www.geeksforgeeks.org/kruskals-minimum-spanning-tree-algorithm-greedy-algo-2/
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<Edge> Kruskal(Point3D[] points)
        {
            List<Edge> resultEdges = new List<Edge>();
            
            List<Edge> sortedEdges = GetAllEdges(points);
            sortedEdges.Sort();

            int[] set = new int[n];
            for (int i = 0; i < n; i++)
                set[i] = i; //at first, each vertex belong to his own set

            while(sortedEdges.Count>0)
            {
                //extract the shortest edge from the list
                double distance = sortedEdges.ElementAt(0).Distance;
                int from = sortedEdges.ElementAt(0).From;
                int to = sortedEdges.ElementAt(0).To;

                if (set[from] != set[to])//if the edge endings don't belong to the same component
                {
                    resultEdges.Add(new Edge(distance, from, to));//add the edge to MST

                    //union the 2 sets together (slow version, this can be optimized)
                    int st = set[to];
                    for (int k = 0; k < n; k++)
                        if (set[k] == st)
                            set[k] = set[from];
                }
                sortedEdges.Remove(sortedEdges.ElementAt(0));//remove the shortest edge after
            }

            return resultEdges;
        }

        private List<Edge> GetAllEdges(Point3D[] points)
        {
            List<Edge> edges = new List<Edge>();

            for (int i = 0; i < points.Length-1; i++)
                for (int j = i+1; j < points.Length; j++)
                {
                    double distance = points[i].DistanceE(points[j]);
                    int from = i;
                    int to = j;
                    edges.Add(new Edge(distance, from, to));
                }

            return edges;
        }

        private void DrawPointsAndEdges2D(Point3D[] points, List<Edge> edges)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics grp = Graphics.FromImage(bmp);

            //Draw the points
            for (int i = 0; i < points.Length; i++)
                grp.DrawEllipse(new Pen(Color.Blue, 3), points[i].X, points[i].Y, 3, 3);

            //Draw the edges
            for (int i = 0; i < edges.Count; i++)
                grp.DrawLine(new Pen(Color.Black, 3), points[edges[i].From].X, points[edges[i].From].Y, points[edges[i].To].X, points[edges[i].To].Y);

            pictureBox1.Image = bmp;
        }
    }
}