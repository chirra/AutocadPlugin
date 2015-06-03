using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class LayerRepository
    {

        private static ObservableCollection<Layer> _layers;

        public static ObservableCollection<Layer> AllLayers
        {
            get
            {
                if (_layers == null)
                    _layers = GenerateClientRepository();
                return _layers;
            }
        }

        private static ObservableCollection<Layer> GenerateClientRepository()
        {
            Shape rectangle = new Shape("rectangle");
            Shape circle = new Shape("circle");
            Shape point = new Shape("point");


            Layer layer1 = new Layer("Jhon", "Doe");
            layer1.Shapes = new List<Shape>();
            layer1.Shapes.Add(rectangle);
            layer1.Shapes.Add(point);

            Layer layer2 = new Layer("Tom", "Ronald");
            layer1.Shapes = new List<Shape>();
            layer1.Shapes.Add(circle);
            layer1.Shapes.Add(point);

            
            ObservableCollection<Layer> layers = new ObservableCollection<Layer>();
            layers.Add(layer1);
            layers.Add(layer2);
            return layers;
        }
    }
}
