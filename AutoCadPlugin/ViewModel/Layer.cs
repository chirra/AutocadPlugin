using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class Layer: ViewModelBase
    {
        private List<Shape> shapes;

        public List<Shape> Shapes
        {
            get
            {
                return shapes;
            }
            set
            {
                shapes = value;
                OnPropertyChanged("Shapes");
            }
        }

        public string Name { get; set; }

        public Layer(string name)
        {
            Name = name;
            Shapes = new List<Shape>()
            {
                new Shape("rectangle"),
                new Shape("circle")
            };
        }
    }
}
