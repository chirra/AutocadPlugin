using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;


namespace AutoCadPlugin
{
    public class Commands : IExtensionApplication
    {
        // функция инициализации (выполняется при загрузке плагина)
        public void Initialize()
        {
            MessageBox.Show("Hello!");
        }

        // функция, выполняемая при выгрузке плагина
        public void Terminate()
        {
            MessageBox.Show("Goodbye!");
        }

        // эта функция будет вызываться при выполнении в AutoCAD команды «TestCommand»
        [CommandMethod("TestCommand")]
        public void MyCommand()
        {
            // получаем текущий документ и его БД
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
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
                    acLyrTblRec.Name = "HabrLayer";

                    // заносим созданный слой в таблицу слоев
                    acLyrTbl.Add(acLyrTblRec);
                    

                    // добавляем созданный слой в документ
                    tr.AddNewlyCreatedDBObject(acLyrTblRec, true);

                    // фиксируем транзакцию
                    tr.Commit();
                }
            }

        }
    }
}