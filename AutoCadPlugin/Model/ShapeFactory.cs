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
        /// <summary>
        /// Return shape of concrete type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Shape CreateShape(ShapeType type, string id, ArrayList parameters)
        {

            switch (type)
            {
                case ShapeType.Point:
                    return new Point(id, (double) parameters[0], (double) parameters[1], (double) parameters[2]);
                    
                case ShapeType.Line:
                    return new Line(id,
                        new Point(id, (double)parameters[0], (double)parameters[1], (double)parameters[2]), 
                        new Point(id, (double)parameters[3], (double)parameters[4], (double)parameters[5])
                        );
                    
                case ShapeType.Circle:
                    return new Circle(id, new Point(id, (double)parameters[0], (double)parameters[1], (double)parameters[2]), (double)parameters[3]);
          
                default:
                    return null;
            }
            
        }
    }
}
