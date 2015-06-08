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
        public string Id { get; set; }

        private List<ObservableShape> _observableShapes;
        public List<ObservableShape> ObservableShapes
        {
            get
            {
                if (_observableShapes == null)
                {
                   _observableShapes  = new List<ObservableShape>();
                    //_observableShapes.Add(new ObservableShape("rectangle"));
                }

              
                return _observableShapes;
            }
            set
            {
                _observableShapes = value;
                OnPropertyChanged("ObservableShapes");
            }
        }

        
        public string Name { get; set; }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        private int _transparency;
        public int Transparency
        {
            get { return _transparency; }
            set
            {
                _transparency = value;
                OnPropertyChanged("Transparency");
            }
        }

        public ObservableLayer(string id, string name, string color, int transparency)
        {
            Id = id;
            Name = name;
            Color = color;
            Transparency = transparency;
        }

        public ObservableLayer(string id, string name, string color, int transparency, IList<ObservableShape> observableShapes ):this(id, name, color, transparency)
        {
            foreach (var observableShape in observableShapes)
            {
                ObservableShapes.Add(observableShape);
            }
            
        }
    }
}
