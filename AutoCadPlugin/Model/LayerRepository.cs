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
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;

namespace AutoCadPlugin.Model
{
    internal class LayerRepository
    {

        private static IList<Layer> _layers;

        public static IList<Layer> AllLayers
        {
            get
            {
                // if (_layers == null)
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
                try
                {
                    using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                    {
                        // открываем таблицу слоев документа
                        //LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;
                        LayerTable tblLayer = (LayerTable) tr.GetObject(acCurDb.LayerTableId, OpenMode.ForRead, false);
                        foreach (var layer in tblLayer)
                        {
                            int layerTransparency;
                            LayerTableRecord entLayer = (LayerTableRecord) tr.GetObject(layer, OpenMode.ForRead);
                            try
                            {
                                layerTransparency = (100-entLayer.Transparency.Alpha*100/255);
                            }
                            catch (Exception)
                            {

                                layerTransparency = 0;
                            }

                            _layers.Add(new Layer(entLayer.Id.ToString(), entLayer.Name,
                                '#' + entLayer.Color.ColorValue.Name.Substring(2, 6),
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
                catch (Exception)
                {


                }

            }

            return _layers;


        }

      
        public static IList<Shape> GetShapes(string layerName)
        {
  
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


                            if (acEnt != null)
                            {
                                switch (acSSObj.ObjectId.ObjectClass.DxfName)
                                {

                                    case "LINE":
                                        var autocadLine = (Autodesk.AutoCAD.DatabaseServices.Line) acEnt;
                                        shapes.Add(ShapeFactory.GetShape("line", autocadLine.Id.ToString(),
                                            new ArrayList()
                                            {
                                                autocadLine.StartPoint.X,
                                                autocadLine.StartPoint.Y,
                                                autocadLine.StartPoint.Z,
                                                autocadLine.EndPoint.X,
                                                autocadLine.EndPoint.Y,
                                                autocadLine.EndPoint.Z,
                                            }));
                                        break;

                                    case "CIRCLE":
                                        var autocadCircle = (Autodesk.AutoCAD.DatabaseServices.Circle) acEnt;

                                        shapes.Add(ShapeFactory.GetShape("circle", autocadCircle.Id.ToString(),
                                            new ArrayList()
                                            {
                                                autocadCircle.Center.X,
                                                autocadCircle.Center.Y,
                                                autocadCircle.Center.Z,
                                                autocadCircle.Radius
                                            }));
                                        break;
                                    case "POINT":
                                        var autocadPoint = (Autodesk.AutoCAD.DatabaseServices.DBPoint) acEnt;
                                        shapes.Add(ShapeFactory.GetShape("point", autocadPoint.Id.ToString(),
                                            new ArrayList()
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



        internal static void SaveLayer(string id, ArrayList parameters)
        {
            //IList<Shape> shapes = new List<Shape>();
            string name = (string) parameters[0];
            string color = (string) parameters[1];
            var transparency = (int) parameters[2];

            // Получение текущего документа и базы данных
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // Старт транзакции
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {

                    /* LayerTable lt = acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForRead) as LayerTable;
               
                LayerTableRecord entLayer = acTrans.GetObject(lt[id], OpenMode.ForWrite) as LayerTableRecord;*/

                    LayerTable tblLayer = (LayerTable) tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite);
                    foreach (var layer in tblLayer)
                    {
                        LayerTableRecord entLayer = (LayerTableRecord) tr.GetObject(layer, OpenMode.ForWrite);
                        if (entLayer != null && entLayer.Id.ToString() == id)
                        {
                            ((Autodesk.AutoCAD.DatabaseServices.LayerTableRecord) entLayer).Name = name;
                            Byte alpha = (Byte) (255*(100 - transparency)/100);
                            Transparency trans = new Transparency(alpha);
                            ((Autodesk.AutoCAD.DatabaseServices.LayerTableRecord) entLayer).Transparency =
                                trans;
                            /*((Autodesk.AutoCAD.DatabaseServices.LayerTableRecord) entLayer).Color =
                            Color.FromColorIndex(ColorMethod.ByColor, short.Parse("0xffffff"));*/
                        }
                    }


                    // Сохранение нового объекта в базе данных
                    tr.Commit();
  
                }
                
                acDoc.Editor.ApplyCurDwgLayerTableChanges();
                acDoc.Editor.Regen();
                acDoc.Editor.UpdateScreen();
                // Очистка транзакции
            }
        }


        internal static void SaveShape(string shapeType, string shapeId, ArrayList parameters)
        {

            // Получение текущего документа и базы данных
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // Старт транзакции
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    PromptSelectionResult getSel = acDoc.Editor.SelectAll();

                    if (getSel.Status == PromptStatus.OK)
                    {
                        SelectionSet selSet = getSel.Value;

                        foreach (SelectedObject selObj in selSet)
                        {
                            var entity = tr.GetObject(selObj.ObjectId, OpenMode.ForWrite) as Entity;

                            if (entity != null && entity.Id.ToString() == shapeId)
                            {
                                switch (shapeType)
                                {
                                    case "point":

                                        var autocadPoint = (Autodesk.AutoCAD.DatabaseServices.DBPoint) entity;
                                        autocadPoint.Position = new Point3d((double) parameters[0],
                                            (double) parameters[1],
                                            (double) parameters[2]);
                                        break;

                                    case "line":
                                        var autocadLine = (Autodesk.AutoCAD.DatabaseServices.Line) entity;
                                        autocadLine.StartPoint = new Point3d((double) parameters[0],
                                            (double) parameters[1],
                                            (double) parameters[2]);
                                        autocadLine.EndPoint = new Point3d((double) parameters[3],
                                            (double) parameters[4],
                                            (double) parameters[5]);
                                        break;
                                    case "circle":
                                        var autocadCircle = (Autodesk.AutoCAD.DatabaseServices.Circle) entity;
                                        autocadCircle.Center = new Point3d((double) parameters[0],
                                            (double) parameters[1],
                                            (double) parameters[2]);
                                        autocadCircle.Radius = (double) parameters[3];
                                        break;
                                }
                            }
                        }
                        tr.Commit();
                    }
                    acDoc.Editor.ApplyCurDwgLayerTableChanges();
                    acDoc.Editor.UpdateScreen();
                }
            }

        }


    }//class
}//namespace


