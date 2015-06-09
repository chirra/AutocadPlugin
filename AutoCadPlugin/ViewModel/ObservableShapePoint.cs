using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    internal class ObservableShapePoint : ObservableShape
    {
       private double _x;
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


        private double _z;
        public double Z
        {
            get { return _z; }
            set
            {
                _z = value;
                OnPropertyChanged("Z");
            }
        }
  

        public ObservableShapePoint(string id, double x, double y, double z):base(id)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public override ObservableShapeType Type
        {
            get { return ObservableShapeType.Point; }
        }

        public override String RuType
        {
            get { return "Точка"; }
        }

      
    }
}
