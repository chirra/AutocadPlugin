using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Point: Shape
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(string id, double x, double y, double z):base(id)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override ShapeType Type
        {
            get
            {
                return ShapeType.Point;
            }
        }
    }
}
