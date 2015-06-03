using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCadPlugin.Model;

namespace AutoCadPlugin.ViewModel
{
    class ObservableLayer: ViewModelBase
    {
        private List<ObservableShape> _observableShapes;

        public List<ObservableShape> ObservableShapes
        {
            get
            {
                if (_observableShapes == null)
                {
                   _observableShapes  = new List<ObservableShape>();
                    _observableShapes.Add(new ObservableShape("rectangle"));
                }

              
                return _observableShapes;
            }
            set
            {
                _observableShapes = value;
                OnPropertyChanged("_observableShapes");
            }
        }

        public string Name { get; set; }

        public ObservableLayer(string name)
        {
            Name = name;
        }

        public ObservableLayer(string name, IList<ObservableShape> observableShapes ):this(name)
        {
            foreach (var observableShape in observableShapes)
            {
                ObservableShapes.Add(observableShape);
            }
            
        }
    }
}
