using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.Windows;
using System.Drawing;
using System.Windows.Forms;
using AutoCadPlugin.View;
using MessageBox = System.Windows.MessageBox;


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
        // эта функция будет вызываться при выполнении в AutoCAD команды «TestCommand»
        [CommandMethod("Autocad")]
        public void MyCommand()
        {

            if (_ps == null)
      {
        // Create the palette set
 
        _ps = new PaletteSet("WPF Palette");
        _ps.Size = new Size(400, 600);
        _ps.DockEnabled =
          (DockSides)((int)DockSides.Left + (int)DockSides.Right);
 
        // Create our first user control instance and
        // host it on a palette using AddVisual()
 
        MainWindow uc = new MainWindow();
        _ps.AddVisual("AddVisual", uc);
 
        // Create our second user control instance and
        // host it in an ElementHost, which allows
        // interop between WinForms and WPF
 
        /*MainWindow uc2 = new MainWindow();
        ElementHost host = new ElementHost();
        host.AutoSize = true;
        host.Dock = DockStyle.Fill;
        host.Child = uc2;
        _ps.Add("Add ElementHost", host);*/
      }
 
      // Display our palette set
 
      _ps.KeepFocus = true;
      _ps.Visible = true;



            // получаем текущий документ и его БД
            /*Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    // создаем новый слой и задаем ему имя
                    LayerTableRecord acLyrTblRec = new LayerTableRecord();
                    acLyrTblRec.Type = "HabrLayer";

                    // заносим созданный слой в таблицу слоев
                    acLyrTbl.Add(acLyrTblRec);
                    

                    // добавляем созданный слой в документ
                    tr.AddNewlyCreatedDBObject(acLyrTblRec, true);

                    // фиксируем транзакцию
                    tr.Commit();*/
               
            

        }
    }
}