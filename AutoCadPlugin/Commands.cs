using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using System.Drawing;
using AutoCadPlugin.View;


namespace AutoCadPlugin
{
    public class Commands : IExtensionApplication
    {
        // функция инициализации (выполняется при загрузке плагина)
        public void Initialize()
        {
            //MessageBox.Show("Hello!");
        }

        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            //MessageBox.Show("Goodbye!");
        }

        static PaletteSet _ps = null;


        // эта функция будет вызываться при выполнении в AutoCAD команды «Autocad»
        [CommandMethod("Autocad")]
        public void MyCommand()
        {

            if (_ps == null)
            {
                // Create the palette set

                _ps = new PaletteSet("WPF Palette");
                _ps.Size = new Size(400, 600);
                _ps.DockEnabled =
                    (DockSides) ((int) DockSides.Left + (int) DockSides.Right);

                // Create our first user control instance and
                // host it on a palette using AddVisual()

                MainWindow uc = new MainWindow();
                _ps.AddVisual("AddVisual", uc);

            }

            // Display our palette set

            _ps.KeepFocus = true;
            _ps.Visible = true;


        }
    }
}