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

        //Collection of Shapes
        private List<ObservableShape> _observableShapes;
        public List<ObservableShape> ObservableShapes
        {
            get { return _observableShapes ?? (_observableShapes = new List<ObservableShape>()); }
            set
            {
                _observableShapes = value;
                OnPropertyChanged("ObservableShapes");
            }
        }

        
        public string Name { get; set; }

        public string Color { get; set; }

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

      
    }
}
