using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.Model
{
    class Layer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Shape> Shapes { get; set; }

        public Layer()
        {
        }

        public Layer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
