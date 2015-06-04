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
        public string Name { get; set; }
        public IList<Shape> Shapes { get; set; }

        private byte transparency = 0;
        public byte Transparency
        {
            get { return transparency; }
            set
            {
                if (value < 0) transparency = 0;
                else if (value >255) transparency = 255;
                else transparency = value;
            }
        }

        private string color = "#ffffff";

        public string Color
        {
            get { return color; } 
            set { color = value; } 
        }


        public Layer(string name)
        {
            Name = name;
            Color = "#ffffff";
            Transparency = 0;
        }

        public Layer(string name, string color, byte transparency): this(name)
        {
            Color = color;
            Transparency = transparency;
        }


        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
