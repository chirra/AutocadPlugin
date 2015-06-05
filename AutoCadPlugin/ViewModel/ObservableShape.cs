using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableShape: ViewModelBase
    {
        private string name = string.Empty;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Type");
            }
        }

        public ObservableShape(string name)
        {
            Name = name;
        }
    }
}
