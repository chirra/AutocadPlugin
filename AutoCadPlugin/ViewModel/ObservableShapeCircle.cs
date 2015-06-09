using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableShapeCircle: ObservableShape
    {

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


        public ObservableShapeCircle(string id, ObservableShapePoint centerPoint, double radius): base(id)
        {
            CenterPoint = centerPoint;
            Radius = radius;
        }

        public override ObservableShapeType Type
        {
            get { return ObservableShapeType.Circle; }
        }

        public override string RuType
        {
            get { return "Окружность"; }
        }
    }
}
