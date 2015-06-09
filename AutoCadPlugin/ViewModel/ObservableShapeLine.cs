using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableShapeLine: ObservableShape
    {
        private ObservableShapePoint _startPoint;
        public ObservableShapePoint StartPoint
        {
            get { return _startPoint;}
            set
            {
                _startPoint = value;
                OnPropertyChanged("StartPoint");
            }
        }

        private ObservableShapePoint _endPoint;
        public ObservableShapePoint EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged("EndPoint");
            }
        }


        public ObservableShapeLine(string id, ObservableShapePoint startPoint, ObservableShapePoint endPoint):base(id)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public override ObservableShapeType Type
        {
            get { return ObservableShapeType.Line; }
        }

        public override string RuType
        {
            get { return "Отрезок"; }
        }
    }
}
