using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoCadPlugin.Infrastructure;
using AutoCadPlugin.Model;
using AutoCadPlugin.View;
using Point = AutoCadPlugin.Model.Point;

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
                    _observableLayers = loadLayers();
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

        private ObservableCollection<ObservableLayer> loadLayers()
        {
            ObservableCollection<ObservableLayer> observableLayers = new ObservableCollection<ObservableLayer>();
            foreach (var layer in Model.DAL.GetLayers())
            {

                ObservableLayer observableLayer = new ObservableLayer(layer.Id, layer.Name, layer.Color, layer.Transparency);
                foreach (var shape in Model.DAL.GetShapes(layer.Name))
                {
                    if (shape.Type == "circle")
                    {
                        ObservableShape observableShape = new ObservableShapeCircle(((Circle)shape).Id,
                            new ObservableShapePoint(((Circle)shape).CenterPoint.Id, ((Circle)shape).CenterPoint.X, ((Circle)shape).CenterPoint.Y, ((Circle)shape).CenterPoint.Z),
                            ((Circle)shape).Radius);

                        observableLayer.ObservableShapes.Add(observableShape);
                    }
                    else if (shape.Type == "point")
                    {
                        ObservableShape observableShape = new ObservableShapePoint(((Point)shape).Id, ((Point)shape).X, ((Point)shape).Y, ((Point)shape).Z);

                        observableLayer.ObservableShapes.Add(observableShape);
                    }
                    else if (shape.Type == "line")
                    {
                        ObservableShape observableShape = new ObservableShapeLine(((Line)shape).Id,
                            new ObservableShapePoint(((Line)shape).StartPoint.Id, ((Line)shape).StartPoint.X, ((Line)shape).StartPoint.Y, ((Line)shape).StartPoint.Z),
                            new ObservableShapePoint(((Line)shape).EndPoint.Id, ((Line)shape).EndPoint.X, ((Line)shape).EndPoint.Y, ((Line)shape).EndPoint.Z));

                        observableLayer.ObservableShapes.Add(observableShape);
                    }
                }

                observableLayers.Add(observableLayer);
            }
            return observableLayers;
        }

        ICommand _commandRefresh;
        public ICommand CommandRefresh
        {
            get
            {
                
                //if (_commandRefresh == null)
                    //_commandRefresh = new RelayCommand(RefreshObservableLayers, CanRefreshObservableLayers);
                    
                return _commandRefresh;
            }
            set { _commandRefresh = value;}
        }

        public void RefreshObservableLayers(object parameter)
        {
            //_observableLayers = null;
            ObservableLayers = loadLayers();
        }

        private ICommand _commandApply;

        public ICommand CommandApply
        {
            get
            {
                /*if (_commandApply == null)
                    _commandApply = new RelayCommand(ApplyChanges);*/
                
                return _commandApply;
            }
            set
            {
                _commandApply = value;
            }
        }

        public MainWindowViewModel()
        {
            CommandApply = new RelayCommand(new Action<object>(ApplyChanges));
            CommandRefresh = new RelayCommand(RefreshObservableLayers);
        }

        public void ApplyChanges(object parameter)
        {
            foreach (var observableLayer in ObservableLayers)
            {
                Model.DAL.SaveLayer(observableLayer.Id,
                    new ArrayList() {observableLayer.Name, observableLayer.Color, observableLayer.Transparency});
                foreach (var observableShape in observableLayer.ObservableShapes)
                    if (observableShape.Type == "point")
                        Model.DAL.SaveShape("point", observableShape.Id,
                            new ArrayList()
                            {
                                ((ObservableShapePoint) observableShape).X,
                                ((ObservableShapePoint) observableShape).Y,
                                ((ObservableShapePoint) observableShape).Z
                            });
                    else if(observableShape.Type == "line")
                        Model.DAL.SaveShape("line", observableShape.Id,
                            new ArrayList()
                            {
                                ((ObservableShapeLine) observableShape).StartPoint.X,
                                ((ObservableShapeLine) observableShape).StartPoint.Y,
                                ((ObservableShapeLine) observableShape).StartPoint.Z,
                                ((ObservableShapeLine) observableShape).EndPoint.X,
                                ((ObservableShapeLine) observableShape).EndPoint.Y,
                                ((ObservableShapeLine) observableShape).EndPoint.Z
                            });
                    else if(observableShape.Type == "circle")
                        Model.DAL.SaveShape("circle", observableShape.Id,
                            new ArrayList()
                            {
                                ((ObservableShapeCircle) observableShape).CenterPoint.X,
                                ((ObservableShapeCircle) observableShape).CenterPoint.Y,
                                ((ObservableShapeCircle) observableShape).CenterPoint.Z,
                                ((ObservableShapeCircle) observableShape).Radius,
                            });
            }
        }

        public bool CanApplyChanges(object parameter)
        {
            if (_observableLayers == null)
                return false;
            else return true;
        }

 
    }
}
