using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Layer
    {
        public string Name { get; set; }
        public IList<Shape> Shapes { get; set; }

        public Layer(string name)
        {
            Name = name;
        }

        

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
