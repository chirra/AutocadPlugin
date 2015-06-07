using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Shape
   {
        public string Id { get; set; }
        public string Type { get; set; }

        public Shape(string type, string id)
        {
            Id = id;
            Type = type;
        }

        public override string ToString()
        {
            return Type;
        }
   }
}
