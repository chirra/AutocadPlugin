using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableShapeLine: ObservableShape
    {
        private double _x1;
        public double X1
        {
            get { return _x1; }
            set
            {
                _x1 = value;
                OnPropertyChanged("X1");
            }
        }

        private double _y1;
        public double Y1
        {
            get { return _y1; }
            set
            {
                _y1 = value;
                OnPropertyChanged("Y1");
            }
        }

        private double _x2;
        public double X2
        {
            get { return _x2; }
            set
            {
                _x2 = value;
                OnPropertyChanged("X2");
            }
        }

        private double _y2;
        public double Y2
        {
            get { return _y2; }
            set
            {
                _y2 = value;
                OnPropertyChanged("Y2");
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

        public ObservableShapeLine(string name, double x1, double y1, double x2, double y2, double h):base(name)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            H = h;
        }
    }
}
