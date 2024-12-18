namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills02 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 02 Skills code goes here

            //1a. Pick single element
            Reference pickRef = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select Element");
            Element pickElement = doc.GetElement(pickRef);

            //1.b Pick multiple elements
            List<Element> pickList = uidoc.Selection.PickElementsByRectangle("Select Elements").ToList();

            TaskDialog.Show("Test", $"I selected {pickList.Count} elements.");

            //2.a Filter selected elements
            List<CurveElement> allCurves = new List<CurveElement>();
            foreach (Element elem in pickList)
            {
                if (elem is CurveElement)
                {
                    allCurves.Add(elem as CurveElement);
                }
            }

            //2.b Filter selected elements for model curves
            List<CurveElement> modelCurves = new List<CurveElement>();
            foreach (Element elem2 in pickList)
            {
                if (elem2 is CurveElement)
                {
                    //CurveElement curveElem = elem2 as CurveElement; This line is the same as the one below
                    CurveElement curveElement = (CurveElement)elem2;

                    if (curveElement.CurveElementType == CurveElementType.ModelCurve)
                    {
                        modelCurves.Add(curveElement);
                    }

                }

            }

            // 3. curve data
            foreach (CurveElement currentCurve in modelCurves)
            {
                Curve curve = currentCurve.GeometryCurve;
                XYZ startPoint = curve.GetEndPoint(0);
                XYZ endPoint = curve.GetEndPoint(1);

                GraphicsStyle curStyle = currentCurve.LineStyle as GraphicsStyle;

                Debug.Print(curStyle.Name);
            }

            /* we need to create transaction before creating something in Revit
            Transaction t = new Transaction(doc);
            t.Start("create a wall");*/

            // but actually is better to create transtactions with using statement
            using (Transaction t = new Transaction(doc))

            {
                t.Start("create a wall");
                // 4. create wall
                Level newLevel = Level.Create(doc, 20);

                CurveElement curveElem = modelCurves[0];
                Curve curCurve = curveElem.GeometryCurve;
                Curve curCurve2 = modelCurves[0].GeometryCurve;

                Wall newWall = Wall.Create(doc, curCurve, newLevel.Id, false);

                // 4.b create wall with wall type
                FilteredElementCollector wallTypes = new FilteredElementCollector(doc);
                wallTypes.OfClass(typeof(WallType));
                /* you can also put this:
                wallTypes.OfCategory(BuiltInCategory.OST_Walls);
                wallTypes.WhereElementIsElementType(); */

                Curve curCurve3 = modelCurves[1].GeometryCurve;
                Wall newWall2 = Wall.Create(doc, curCurve3, wallTypes.FirstElementId(), newLevel.Id, 20, 0, false, false);
                

                t.Commit();
            }


            return Result.Succeeded;
        }
    }

}