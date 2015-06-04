using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;

namespace AutoCadPlugin.Model
{
    class LayerRepository
    {

        private static IList<Layer> _layers;

        public static IList<Layer> AllLayers
        {
            get
            {
                if (_layers == null)
                    _layers = GenerateLayerRepository();
                return _layers;
            }
        }

        private static IList<Layer> GenerateLayerRepository()
        {

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            _layers = new List<Layer>();

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
                        byte layerTransparency = 0;
                        LayerTableRecord entLayer = (LayerTableRecord)tr.GetObject(layer, OpenMode.ForRead);
                        try
                        {
                            layerTransparency = entLayer.Transparency.Alpha;
                        }
                        catch (Exception)
                        {

                            layerTransparency = 0;
                        }

                        _layers.Add(new Layer(entLayer.Name, '#' + entLayer.Color.ColorValue.Name.Substring(2, 6), layerTransparency));
                    }


                   
                    

                    /*// создаем новый слой и задаем ему имя
                    LayerTableRecord acLyrTblRec = new LayerTableRecord();
                    acLyrTblRec.Name = "HabrLayer";

                    // заносим созданный слой в таблицу слоев
                    acLyrTbl.Add(acLyrTblRec);

                    // добавляем созданный слой в документ
                    tr.AddNewlyCreatedDBObject(acLyrTblRec, true);

                    // фиксируем транзакцию
                    tr.Commit();*/
                }
            }

            return _layers;

           
        }


        public static void FilterSelectionSet(string layerName)
        {
             Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // ''Получение редактора текущего документа
                Editor acDocEd =
                    Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

                // Создание массива TypedValue для определение критериев фильтра
                TypedValue[] acTypValAr = new TypedValue[1];
                acTypValAr.SetValue(new TypedValue((int) DxfCode.LayerName, layerName), 0);

                // Назначение критериев фильтра объекту SelectionFilter
                SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);

                // Запрос выбора объектов на чертеже
                PromptSelectionResult acSSPrompt;
                acSSPrompt = acDocEd.SelectAll(acSelFtr);

                // Если статус запрса OK, объекты выбраны
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;

                    

                    Application.ShowAlertDialog("Количество выбранных объектов: " +
                                                acSSet.Count.ToString());
                }
                else
                {
                    Application.ShowAlertDialog("Количество выбранных объектов: 0");
                }
            }
        }
    }
}
