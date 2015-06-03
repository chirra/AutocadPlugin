using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Shape
   {
        public string Name { get; set; }

        public Shape(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
   }
}
