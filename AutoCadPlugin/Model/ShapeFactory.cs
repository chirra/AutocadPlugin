using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace AutoCadPlugin.Model
{
    class ShapeFactory
    {

        public static Shape GetShape(string type, ArrayList parameters)
        {
            Shape result = new Shape(type);

            switch (type.ToLower().Trim())
            {
                case "point":
                    result = new Point((double) parameters[0], (double) parameters[1], (double) parameters[2]);
                    break;
                case "line":
                    result = new Line(
                        new Point((double)parameters[0], (double)parameters[1], (double)parameters[2]), 
                        new Point((double)parameters[3], (double)parameters[4], (double)parameters[5])
                        );
                    break;
                case "circle":
                    result = new Circle(new Point((double)parameters[0], (double)parameters[1], (double)parameters[2]), (double)parameters[3]);
                    break;
            }

            return result;
        }
    }
}
