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


        private double _h;
        public double H
        {
            get { return _h; }
            set
            {
                _h = value;
                OnPropertyChanged("H");
            }
        }
        



        public ObservableShapePoint(string name, double x, double y, double h):base(name)
        {
            X = x;
            Y = y;
            H = h;
        }

    }
}
