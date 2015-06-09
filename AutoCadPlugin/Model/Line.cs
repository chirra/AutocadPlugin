using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Line: Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Line(string id, Point startPoint, Point endPoint):base(id)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public override ShapeType Type
        {
            get { return ShapeType.Line; }
        }
    }
}

