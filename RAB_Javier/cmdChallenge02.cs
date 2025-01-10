using System.Xaml.Schema;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;

namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge02 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 02 Challenge code goes here
            // 1. Select Elements
            TaskDialog.Show("Select lines", "Select some lines to convert to Revit elements");
            List<Element> pickList = uidoc.Selection.PickElementsByRectangle("Select Elements").ToList();

            // 2. Filter elements for curves and Filter curves for Model Curves
            List<CurveElement> filteredList = new List<CurveElement>();
            foreach (Element elem in pickList)
            {
                if (elem is CurveElement)
                {
                    CurveElement curCurveElem = elem as CurveElement;
                    filteredList.Add(curCurveElem);
                }

            }

            // 3. Get View and Level
            View curView = doc.ActiveView;
            Parameter levelParam = curView.LookupParameter("Associated Level");
            string levelName = levelParam.AsString();
            ElementId levelId = levelParam.AsElementId();

            Level wantedLevel = GetLevelByName(doc, levelName);

            // 4. Get types
            WallType wt1 = GetWallTypeByName(doc, "Storefront");
            WallType wt2 = GetWallTypeByName(doc, "Generic 8\"");
            MEPSystemType ductSystem = GetMEPSystemByName(doc, "Supply Air");
            MEPSystemType pipeSystem = GetMEPSystemByName(doc, "Domestic Hot Water");
            DuctType ductType = GetDuctTypeByName(doc, "Default");
            PipeType pipeType = GetPipeTypeByName(doc, "Default");

            // 5.Loop through curve elements
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Create elements");
                
                foreach (CurveElement currentCurve in filteredList)
                {
                    // 6. get graphicstyle from line
                    GraphicsStyle currentStyle = currentCurve.LineStyle as GraphicsStyle;
                    string lineStyleName = currentStyle.Name;

                    // 6.b.get curve geometry
                    Curve curveGeom = currentCurve.GeometryCurve;

                    // 7. use switch statement to create elements

                }

                t.Commit();
            }












            return Result.Succeeded;
        }

        private PipeType GetPipeTypeByName(Document doc, string pipeTypeName)
        {
            FilteredElementCollector pipeCollector = new FilteredElementCollector(doc);
            pipeCollector.OfClass(typeof(PipeType));


            foreach (PipeType curPipeType in pipeCollector)
            {
                if (curPipeType.Name == pipeTypeName)

                {
                    return curPipeType;
                }

            }

            return null;
        }

        private DuctType GetDuctTypeByName(Document doc, string ductTypeName)
        {
            FilteredElementCollector ductCollector = new FilteredElementCollector(doc);
            ductCollector.OfClass(typeof(DuctType));


            foreach (DuctType curDuctType in ductCollector)
            {
                if (curDuctType.Name == ductTypeName)

                {
                    return curDuctType;
                }

            }

            return null;
        }

        private MEPSystemType GetMEPSystemByName(Document doc, string systemTypeName)
        {
            FilteredElementCollector systemCollector = new FilteredElementCollector(doc);
            systemCollector.OfClass(typeof(MEPSystemType));


            foreach (MEPSystemType curSystemType in systemCollector)
            {
                if (curSystemType.Name == systemTypeName)

                {
                    return curSystemType;
                }

            }

            return null;
        }

        private WallType GetWallTypeByName(Document doc, string wallTypeName)
        {
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            //wallCollector.OfCategory(BuiltInCategory.OST_Walls);
            //wallCollector.WhereElementIsElementType();
            wallCollector.OfClass(typeof(WallType)); // this line is the same as the 2 previous ones


            foreach (WallType curWallType in wallCollector)
            {
                if (curWallType.Name == wallTypeName)

                {
                    return curWallType;
                }

            }

            return null;
        }

        internal static Level GetLevelByName(Document doc, string levelName)
        {
            FilteredElementCollector levelCollector = new FilteredElementCollector(doc);
            levelCollector.OfCategory(BuiltInCategory.OST_Levels);
            levelCollector.WhereElementIsNotElementType();

            foreach (Level curLevel in levelCollector)
            {
                if (curLevel.Name == levelName)

                {
                    return curLevel;
                }

            }

            return null;
        }

        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge02";
            string buttonTitle = "Module\r02";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module02,
                Properties.Resources.Module02,
                "Module 02 Challenge");

            return myButtonData.Data;
        }
    }

}
