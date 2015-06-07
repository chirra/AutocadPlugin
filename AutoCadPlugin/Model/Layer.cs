using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using Xceed.Wpf.DataGrid;

namespace AutoCadPlugin.Model
{
    class Layer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<Shape> Shapes { get; set; }

        private byte _transparency = 0;
        public byte Transparency
        {
            get { return _transparency; }
            set
            {
                if (value < 0) _transparency = 0;
                else if (value >255) _transparency = 255;
                else _transparency = value;
            }
        }

        private string _color = "#ffffff";

        public string Color
        {
            get { return _color; } 
            set { _color = value; } 
        }


       public Layer(string id, string name, string color, byte transparency)
        {
            Id = id;
           Name = name;
            Color = color;
            Transparency = transparency;
        }


        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
