using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    abstract class ObservableShape: ViewModelBase
    {
        public string Id { get; set; }
        public abstract ObservableShapeType Type { get; }
        public abstract String RuType { get; }

        protected ObservableShape(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}
