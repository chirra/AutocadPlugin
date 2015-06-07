using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableShapeCircle: ObservableShape
    {
      /*  private double _x;
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }


        private double _y;
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }


        private double _h;
        public double H
        {
            get { return _h; }
            set
            {
                _h = value;
                OnPropertyChanged("H");
            }
        }*/

        private ObservableShapePoint _centerPoint;

        public ObservableShapePoint CenterPoint
        {
            get
            {
                return _centerPoint;
            }
            set
            {
                _centerPoint = value;
                OnPropertyChanged("CenterPoint");
            }
        }

        private double _radius;
        public double Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                OnPropertyChanged("Radius");
            }
        }



       /* public ObservableShapeCircle(string type, double x, double y, double h, double radius):base(type)
        {
            X = x;
            Y = y;
            H = h;
            Radius = radius;
        }    */

        public ObservableShapeCircle(string id, ObservableShapePoint centerPoint, double radius): base("circle", id)
        {
            CenterPoint = centerPoint;
            Radius = radius;
        }
    }
}
