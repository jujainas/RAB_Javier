namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge04 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 04 Challenge code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 04 Challenge", "Coming Soon!");

            //empieza a borrar desde aqui
            // 1. Select Elements
            TaskDialog.Show("Select Elements", "Select Model Lines to convert to Revit Elements");
            List<Element> pickList = uidoc.Selection.PickElementsByRectangle("Select Elements").ToList();

            // 2. Filter elements for curves and Filter curves for Model Curves
            List<CurveElement> filteredList = new List<CurveElement>();
            foreach (Element elem in pickList)
            {
                if (elem is CurveElement curveElement)
            }
            




            // terminar de borrar aqui


            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge04";
            string buttonTitle = "Module\r04";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module04,
                Properties.Resources.Module04,
                "Module 04 Challenge");

            return myButtonData.Data;
        }
    }

}
