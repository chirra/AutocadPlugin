using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    internal abstract class Shape
    {
        public string Id { get; set; }
        public abstract ShapeType Type { get; }

        protected Shape(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
