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
        /*Layer _currentLayer;
        public Layer CurrentLayer
        {
            get
            {
                if (_currentLayer == null)
                    _currentLayer = new Layer();
                return _currentLayer;
            }
            set
            {
                _currentLayer = value;
                OnPropertyChanged("CurrentLayer");
            }
        }*/

        ObservableCollection<Layer> _layers;
        public ObservableCollection<Layer> Layers
        {
            get
            {
                if (_layers == null)
                    //_layers = LayerRepository.AllLayers;
                    _layers = new ObservableCollection<Layer>()
                    {
                        new Layer("Layer1"),
                        new Layer("Layer2")
                    };
                return _layers;
            }
            set
            {
                _layers = value;
                OnPropertyChanged("Layers");
            }
        }

        /*ObservableCollection<Shape> _shapes;

        public ObservableCollection<Shape> Shapes
        {
            get
            {
                if (_shapes = null)
                    _shapes = 
            }
        }*/
    }
}
