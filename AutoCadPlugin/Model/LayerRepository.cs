using System;
using System.Collections;
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
    internal class LayerRepository
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
                    LayerTable tblLayer = (LayerTable) tr.GetObject(acCurDb.LayerTableId, OpenMode.ForRead, false);
                    foreach (var layer in tblLayer)
                    {
                        byte layerTransparency = 0;
                        LayerTableRecord entLayer = (LayerTableRecord) tr.GetObject(layer, OpenMode.ForRead);
                        try
                        {
                            layerTransparency = entLayer.Transparency.Alpha;
                        }
                        catch (Exception)
                        {

                            layerTransparency = 0;
                        }

                        _layers.Add(new Layer(entLayer.Name, '#' + entLayer.Color.ColorValue.Name.Substring(2, 6),
                            layerTransparency));
                    }





                    /*// создаем новый слой и задаем ему имя
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

            return _layers;


        }


        public static IList<Shape> GetShapes(string layerName)
        {
            /* Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
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

                    // Перебор объектов в наборе
                       foreach (SelectedObject acSSObj in acSSet)
                       {
                           // Проверка, нужно убедится в правильности полученного объекта
                           if (acSSObj != null)
                           {
                               // Открытие объекта для записи
                               Entity acEnt = acTrans.GetObject(acSSObj.ObjectId,
                                                                OpenMode.ForWrite) as Entity;
                               if (acEnt != null)
                               {
                                   // Изменение цвета объекта на Зеленый
                                   acEnt.ColorIndex = 3;
                               }
                           }
                       } 

                    Application.ShowAlertDialog("Количество выбранных объектов: " +
                                                acSSet.Count.ToString());
                }
                else
                {
                    Application.ShowAlertDialog("Количество выбранных объектов: 0");
                }
            }*/




            IList<Shape> shapes = new List<Shape>();
            // Получение текущего документа и базы данных
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Старт транзакции
            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                TypedValue[] acTypValAr = new TypedValue[1];
                acTypValAr.SetValue(new TypedValue((int) DxfCode.LayerName, layerName), 0);

                // Назначение критериев фильтра объекту SelectionFilter
                SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);
                // Запрос выбора объектов в области чертежа
                PromptSelectionResult acSSPrompt = acDoc.Editor.SelectAll(acSelFtr);

                // Если статус запроса равен OK, объекты выбраны
                if (acSSPrompt.Status == PromptStatus.OK)
                {
                    SelectionSet acSSet = acSSPrompt.Value;

                    // Перебор объектов в наборе
                    foreach (SelectedObject acSSObj in acSSet)
                    {
                        // Проверка, нужно убедится в правильности полученного объекта
                        if (acSSObj != null)
                        {
                            // Открытие объекта для записи
                            Entity acEnt = acTrans.GetObject(acSSObj.ObjectId,
                                OpenMode.ForRead) as Entity;


                            //Autodesk.AutoCAD.DatabaseServices.Curve autocadCurve;
                            //Autodesk.AutoCAD.DatabaseServices.Line autocadCircle;


                            if (acEnt != null)
                            {
                                 switch (acSSObj.ObjectId.ObjectClass.DxfName)
                                 {
                                     /*case "POINT":
                                         autocadCurve = (Autodesk.AutoCAD.DatabaseServices.point.Point) acEnt;
                                         break;*/
                                     case "LINE":
                                         var autocadLine = (Autodesk.AutoCAD.DatabaseServices.Line) acEnt;
                                         shapes.Add(ShapeFactory.GetShape("line", new ArrayList()
                                         {
                                             autocadLine.StartPoint.X, autocadLine.StartPoint.Y, autocadLine.StartPoint.Z,
                                             autocadLine.EndPoint.X,  autocadLine.EndPoint.Y,  autocadLine.EndPoint.Z,
                                         }));
                                         break;
                                     //default:
                                     case "CIRCLE":
                                         var autocadCircle = (Autodesk.AutoCAD.DatabaseServices.Circle) acEnt;
                                         
                                         shapes.Add(ShapeFactory.GetShape("circle", new ArrayList()
                                         {
                                            autocadCircle.Center.X, autocadCircle.Center.Y, autocadCircle.Center.Z,
                                            autocadCircle.Radius
                                         }));
                                         break;
                                     case "POINT":
                                         var autocadPoint = (Autodesk.AutoCAD.DatabaseServices.DBPoint)acEnt;
                                         shapes.Add(ShapeFactory.GetShape("point", new ArrayList()
                                            {autocadPoint.Position.X, autocadPoint.Position.Y, autocadPoint.Position.Z}));
                                         break;
                                 }
                            }
                        }
                    }

                    // Сохранение нового объекта в базе данных
                    acTrans.Commit();
                }

                // Очистка транзакции
            }
            return shapes;
        }
    }
}

