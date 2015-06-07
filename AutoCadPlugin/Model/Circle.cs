using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Circle: Shape
    {
        public Point CenterPoint { get; set; }
        public double Radius { get; set; }

        public Circle(string id, Point centerPoint, double radius):base("circle", id)
        {
            CenterPoint = centerPoint;
            Radius = radius;
        }
    }
}
