using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoCadPlugin.Infrastructure;
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


        ObservableCollection<ObservableLayer> _observableLayers;
        public ObservableCollection<ObservableLayer> ObservableLayers
        {
            get
            {
                if (_observableLayers == null)
                {
                    _observableLayers = new ObservableCollection<ObservableLayer>();
                    foreach (var layer in LayerRepository.AllLayers)
                    {
                        /*ObservableShape shape1 = new ObservableShapeCircle("Circle", 12, 11, 56, 44);
                        ObservableShape shape2 = new ObservableShapeLine("Line", 12, 11, 56, 44, 33);
                        ObservableShape shape3 = new ObservableShapePoint("Point", 16, 18, 45);
                        ObservableShape shape4 = new ObservableShapeCircle("Circle", 66, 45, 34, 76);
                        */

                        ObservableLayer observableLayer = new ObservableLayer(layer.Name, layer.Color, layer.Transparency);
                        foreach (var shape in LayerRepository.GetShapes(layer.Name))
                        {
                            if (shape.Type == "circle")
                            {
                                ObservableShape observableShape = new ObservableShapeCircle("Circle", ((Circle)shape).CenterPoint.X,
                                    ((Circle)shape).CenterPoint.Y, ((Circle)shape).CenterPoint.Z,
                                    ((Circle)shape).Radius);

                                observableLayer.ObservableShapes.Add(observableShape);
                            }
                            else if (shape.Type == "point")
                            {
                                ObservableShape observableShape = new ObservableShapePoint("Point", ((Point)shape).X,
                                    ((Point)shape).Y, ((Point)shape).Z);

                                observableLayer.ObservableShapes.Add(observableShape);
                            }
                            else if (shape.Type == "line")
                            {
                                ObservableShape observableShape = new ObservableShapeLine("Line", ((Line)shape).StartPoint.X,
                                    ((Line)shape).StartPoint.Y, ((Line)shape).EndPoint.X, ((Line)shape).EndPoint.Y,((Line)shape).EndPoint.Z);

                                observableLayer.ObservableShapes.Add(observableShape);
                            }
                        }

                        _observableLayers.Add(observableLayer);
                    }
                }
                    
                   /* _observableLayers = new ObservableCollection<ObservableLayer>()
                    {
                        new ObservableLayer("Layer1"),
                        new ObservableLayer("Layer2")
                    };*/
                return _observableLayers;
            }
            set
            {
                _observableLayers = value;
                OnPropertyChanged("ObservableLayers");
            }
        }

        RelayCommand _commandRefresh;
        public ICommand CommandRefresh
        {
            get
            {
                if (_commandRefresh == null)
                    //_commandRefresh = new RelayCommand(RefreshObservableLayers, CanRefreshObservableLayers);
                    _commandRefresh = new RelayCommand(RefreshObservableLayers);
                return _commandRefresh;
            }
        }

        public void RefreshObservableLayers(object parameter)
        {
            ObservableLayers = null;
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
