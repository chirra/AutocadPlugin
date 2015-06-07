using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCadPlugin.ViewModel
{
    class ObservableShape: ViewModelBase
    {
        public string Id { get; set; }
        private string type = string.Empty;

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public ObservableShape(string type, string id)
        {
            Type = type;
            Id = id;
        }
    }
}
