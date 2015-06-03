using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCadPlugin.Model;

namespace AutoCadPlugin.ViewModel
{
    class MainWindowViewModel: ViewModelBase
    {
        /*ObservableLayer _currentLayer;
        public ObservableLayer CurrentLayer
        {
            get
            {
                if (_currentLayer == null)
                    _currentLayer = new ObservableLayer();
                return _currentLayer;
            }
            set
            {
                _currentLayer = value;
                OnPropertyChanged("CurrentLayer");
            }
        }*/

        ObservableCollection<ObservableLayer> _layers;
        public ObservableCollection<ObservableLayer> Layers
        {
            get
            {
                if (_layers == null)
                {
                    _layers = new ObservableCollection<ObservableLayer>();
                    foreach(var layer in LayerRepository.AllLayers)
                        _layers.Add(new ObservableLayer(layer.Name));
                }
                    
                   /* _layers = new ObservableCollection<ObservableLayer>()
                    {
                        new ObservableLayer("Layer1"),
                        new ObservableLayer("Layer2")
                    };*/
                return _layers;
            }
            set
            {
                _layers = value;
                OnPropertyChanged("Layers");
            }
        }

        /*ObservableCollection<ObservableShape> _shapes;

        public ObservableCollection<ObservableShape> observableShapes
        {
            get
            {
                if (_shapes = null)
                    _shapes = 
            }
        }*/
    }
}
