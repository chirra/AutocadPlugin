using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableLayer: ViewModelBase
    {
        private List<ObservableShape> _observableShapes;

        public List<ObservableShape> ObservableShapes
        {
            get
            {
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
            ObservableShapes = new List<ObservableShape>()
            {
                new ObservableShape("rectangle"),
                new ObservableShape("circle")
            };
        }
    }
}
