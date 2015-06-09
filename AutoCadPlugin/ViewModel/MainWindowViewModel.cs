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
        //Constructor
        public MainWindowViewModel()
        {
            //Register commands for press buttons events
            CommandApply = new RelayCommand(new Action<object>(ApplyChanges));
            CommandRefresh = new RelayCommand(new Action<object>(RefreshObservableLayers));
        }

        //Property of Layers collection
        ObservableCollection<ObservableLayer> _observableLayers;
        public ObservableCollection<ObservableLayer> ObservableLayers
        {
            get { return _observableLayers ?? (_observableLayers = loadLayers()); }
            set
            {
                _observableLayers = value;
                OnPropertyChanged("ObservableLayers");
            }
        }
        
        
        #region loadLayers method
        private ObservableCollection<ObservableLayer> loadLayers()
        {
            ObservableCollection<ObservableLayer> observableLayers = new ObservableCollection<ObservableLayer>();
            foreach (var layer in Model.Dal.GetLayers())
            {

                ObservableLayer observableLayer = new ObservableLayer(layer.Id, layer.Name, layer.Color, layer.Transparency);
                foreach (var shape in Model.Dal.GetShapes(layer.Name))
                {
                    switch (shape.Type)
                    {
                        case ShapeType.Point:
                            {
                                ObservableShape observableShape = new ObservableShapePoint(
                                    ((Point)shape).Id, 
                                    ((Point)shape).X, 
                                    ((Point)shape).Y, 
                                    ((Point)shape).Z);

                                observableLayer.ObservableShapes.Add(observableShape);
                            }
                            break;

                        case ShapeType.Circle:
                        {
                            ObservableShape observableShape = new ObservableShapeCircle(
                                ((Circle)shape).Id,
                                new ObservableShapePoint(
                                    ((Circle)shape).CenterPoint.Id, 
                                    ((Circle)shape).CenterPoint.X, 
                                    ((Circle)shape).CenterPoint.Y, 
                                    ((Circle)shape).CenterPoint.Z),
                                ((Circle)shape).Radius);

                            observableLayer.ObservableShapes.Add(observableShape);
                        }
                            break;
                        
                        case ShapeType.Line:
                        {
                            ObservableShape observableShape = new ObservableShapeLine(
                                ((Line)shape).Id,
                                new ObservableShapePoint(
                                    ((Line)shape).StartPoint.Id, 
                                    ((Line)shape).StartPoint.X, 
                                    ((Line)shape).StartPoint.Y, 
                                    ((Line)shape).StartPoint.Z),
                                new ObservableShapePoint(
                                    ((Line)shape).EndPoint.Id, 
                                    ((Line)shape).EndPoint.X, 
                                    ((Line)shape).EndPoint.Y, 
                                    ((Line)shape).EndPoint.Z));

                            observableLayer.ObservableShapes.Add(observableShape);
                        }
                            break;
                    }
                }

                observableLayers.Add(observableLayer);
            }
            return observableLayers;
        }
#endregion

        #region Commands
        /// <summary>
        /// Load layers and shapes from the database
        /// </summary>
        public ICommand CommandRefresh { get; set; }
        
        public void RefreshObservableLayers(object parameter)
        {
            ObservableLayers = loadLayers();
        }


        /// <summary>
        /// Save layers and shapes to the database
        /// </summary>
        public ICommand CommandApply { get; set; }
        
        public void ApplyChanges(object parameter)
        {
            foreach (var observableLayer in ObservableLayers)
            {
                Model.Dal.SaveLayer(observableLayer.Id, observableLayer.Name, observableLayer.Color, observableLayer.Transparency);
                foreach (var observableShape in observableLayer.ObservableShapes)
                    switch (observableShape.Type)
                    {
                        case ObservableShapeType.Point:
                            Model.Dal.SaveShape(ShapeType.Point, observableShape.Id,
                                new ArrayList()
                                {
                                    ((ObservableShapePoint) observableShape).X,
                                    ((ObservableShapePoint) observableShape).Y,
                                    ((ObservableShapePoint) observableShape).Z
                                });
                            break;

                        case ObservableShapeType.Line:
                            Model.Dal.SaveShape(ShapeType.Line, observableShape.Id,
                                new ArrayList()
                                {
                                    ((ObservableShapeLine) observableShape).StartPoint.X,
                                    ((ObservableShapeLine) observableShape).StartPoint.Y,
                                    ((ObservableShapeLine) observableShape).StartPoint.Z,
                                    ((ObservableShapeLine) observableShape).EndPoint.X,
                                    ((ObservableShapeLine) observableShape).EndPoint.Y,
                                    ((ObservableShapeLine) observableShape).EndPoint.Z
                                });
                            break;

                        case ObservableShapeType.Circle:
                            Model.Dal.SaveShape(ShapeType.Circle, observableShape.Id,
                                new ArrayList()
                                {
                                    ((ObservableShapeCircle) observableShape).CenterPoint.X,
                                    ((ObservableShapeCircle) observableShape).CenterPoint.Y,
                                    ((ObservableShapeCircle) observableShape).CenterPoint.Z,
                                    ((ObservableShapeCircle) observableShape).Radius,
                                });
                            break;
                    }
            }
        }
        #endregion

    }
}
