using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCadPlugin.Model;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Internal;

namespace AutoCadPlugin.DAL
{
    internal class DalLayers
    {

        public IList<string> GetLayers()
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            List<string> result = new List<string>();

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    //LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    LayerTable tblLayer = (LayerTable) tr.GetObject(acCurDb.LayerTableId, OpenMode.ForRead, false);
                    foreach (var layer in tblLayer)
                    {
                        LayerTableRecord entLayer = (LayerTableRecord) tr.GetObject(layer, OpenMode.ForRead);
                        result.Add(entLayer.Id.ToString());
                    }

                }
            }
            return result;
        }

        public ArrayList GetLayerProperties(string layerId, string[] properties)
        {
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            ArrayList result = new ArrayList();

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    //LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                    LayerTable tblLayer = (LayerTable)tr.GetObject(acCurDb.LayerTableId, OpenMode.ForRead, false);

                   foreach (var layer in tblLayer)
                    {
                        LayerTableRecord entLayer = (LayerTableRecord)tr.GetObject(layer, OpenMode.ForRead);
                       if (entLayer.Id.ToString() == layerId)
                           foreach (var prop in properties)
                           {
                               
                           }

                    }

                }
            }
            return result;
        }
    }
}


