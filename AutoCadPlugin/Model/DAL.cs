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
    internal class Dal
    {
        #region GetLayers
        /// <summary>
        /// Return layers collection
        /// </summary>
        /// <returns></returns>
        public static IList<Layer> GetLayers()
        {

            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            IList<Layer> layers = new List<Layer>();

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    // открываем таблицу слоев документа
                    LayerTable tblLayer = (LayerTable) tr.GetObject(acCurDb.LayerTableId, OpenMode.ForRead, false);
                    foreach (var layer in tblLayer)
                    {
                        LayerTableRecord entLayer = (LayerTableRecord) tr.GetObject(layer, OpenMode.ForRead);
                        int layerTransparency;
                        try
                        {
                            layerTransparency = (100 - entLayer.Transparency.Alpha*100/255);
                        }
                        catch (Exception)
                        {

                            layerTransparency = 0;
                        }

                        layers.Add(new Layer(entLayer.Id.ToString(), entLayer.Name,
                            '#' + entLayer.Color.ColorValue.Name.Substring(2, 6),
                            layerTransparency));
                    }
                }
            }

            return layers;
        }
#endregion

        #region GetShapes
        /// <summary>
        /// Return Shapes collection of the layer
        /// </summary>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static IList<Shape> GetShapes(string layerName)
        {
  
            IList<Shape> shapes = new List<Shape>();
            // Получение текущего документа и базы данных
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // Старт транзакции
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    // Назначение критериев фильтра объекту SelectionFilter
                    TypedValue[] acTypValAr = new TypedValue[1];
                    acTypValAr.SetValue(new TypedValue((int) DxfCode.LayerName, layerName), 0);
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
                                // Открытие объекта для чтения
                                Entity acEnt = acTrans.GetObject(acSSObj.ObjectId,
                                    OpenMode.ForRead) as Entity;


                                if (acEnt != null)
                                {
                                    switch (acSSObj.ObjectId.ObjectClass.DxfName)
                                    {
                                        case "POINT":
                                            var autocadPoint = (Autodesk.AutoCAD.DatabaseServices.DBPoint)acEnt;
                                            shapes.Add(ShapeFactory.CreateShape(ShapeType.Point, autocadPoint.Id.ToString(),
                                                new ArrayList()
                                                {
                                                    autocadPoint.Position.X,
                                                    autocadPoint.Position.Y,
                                                    autocadPoint.Position.Z
                                                }));
                                            break;

                                        case "LINE":
                                            var autocadLine = (Autodesk.AutoCAD.DatabaseServices.Line) acEnt;

                                            shapes.Add(ShapeFactory.CreateShape(ShapeType.Line, autocadLine.Id.ToString(),
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

                                            shapes.Add(ShapeFactory.CreateShape(ShapeType.Circle,
                                                autocadCircle.Id.ToString(),
                                                new ArrayList()
                                                {
                                                    autocadCircle.Center.X,
                                                    autocadCircle.Center.Y,
                                                    autocadCircle.Center.Z,
                                                    autocadCircle.Radius
                                                }));
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
            }
            return shapes;
        }
#endregion

        #region SaveLayer
        /// <summary>
        /// Save layer to the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="transparency"></param>
        internal static void SaveLayer(string id, string name, string color, int transparency)
        {

            // Получение текущего документа и базы данных
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // Старт транзакции
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                    LayerTable tblLayer = (LayerTable) tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite);
                    foreach (var layer in tblLayer)
                    {
                        LayerTableRecord entLayer = (LayerTableRecord) tr.GetObject(layer, OpenMode.ForWrite);
                        if (entLayer != null && entLayer.Id.ToString() == id)
                        {
                            entLayer.Name = name;
                            entLayer.Transparency = new Transparency((Byte)(255 * (100 - transparency) / 100));
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
        #endregion

        #region SaveShape
        internal static void SaveShape(ShapeType shapeType, string shapeId, ArrayList parameters)
        {

            // Получение текущего документа и базы данных
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;

            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // Старт транзакции
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
                                    case ShapeType.Point:
                                        var autocadPoint = (Autodesk.AutoCAD.DatabaseServices.DBPoint) entity;

                                        autocadPoint.Position = new Point3d(
                                            (double) parameters[0],
                                            (double) parameters[1],
                                            (double) parameters[2]);

                                        break;

                                    case ShapeType.Line:
                                        var autocadLine = (Autodesk.AutoCAD.DatabaseServices.Line) entity;

                                        autocadLine.StartPoint = new Point3d(
                                            (double) parameters[0],
                                            (double) parameters[1],
                                            (double) parameters[2]);

                                        autocadLine.EndPoint = new Point3d(
                                            (double) parameters[3],
                                            (double) parameters[4],
                                            (double) parameters[5]);

                                        break;

                                    case ShapeType.Circle:
                                        var autocadCircle = (Autodesk.AutoCAD.DatabaseServices.Circle) entity;

                                        autocadCircle.Center = new Point3d(
                                            (double) parameters[0],
                                            (double) parameters[1],
                                            (double) parameters[2]);

                                        autocadCircle.Radius = (double) parameters[3];
                                        break;

                                    default:
                                        throw new ArgumentException();
                                }//switch
                            }//if
                        }//foreach
                        tr.Commit();
                    }
                    acDoc.Editor.ApplyCurDwgLayerTableChanges();
                    acDoc.Editor.UpdateScreen();
                }
            }

        }
        #endregion

    }//class
}//namespace


