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
       // private string _type = string.Empty;

        public abstract ObservableShapeType Type { get; }
        //get { return _type; }
            /*set
            {
                _type = value;
                OnPropertyChanged("Type");
            }*/ }

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
