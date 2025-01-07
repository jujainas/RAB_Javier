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
            // Select Elements
            List<Element> pickList = uidoc.Selection.PickElementsByRectangle("Select Elements").ToList();

            // Filter elements for curves and Filter curves for Model Curves
            List<CurveElement> modelCurves = new List<CurveElement>();
            foreach (Element elem in pickList)
            {
                if (elem is CurveElement)
                {
                    CurveElement curveElem = elem as CurveElement;

                    if (curveElem.CurveElementType == CurveElementType.ModelCurve)
                    {
                        modelCurves.Add(curveElem);
                    }
                }

            }

            // 





            return Result.Succeeded;
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
