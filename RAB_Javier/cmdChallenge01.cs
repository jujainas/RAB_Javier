namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // create a transaction to lock the model
            Transaction t = new Transaction(doc);
            t.Start("I am doing something in Revit");

            // Your Module 01 Challenge code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Challenge", "Hope it works!");



            // Declare a number variable and set it to 250
            int number = 250;

            //Declare a starting elevation variable and set it to 0
            double startElev = 0;

            //Declare a floor height variable and set it to 15 
            double floorHeight = 15;



            // create a filtered element collector to get floor plan and ceiling plan view family type
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            collector1.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorPlanVFT = null;
            foreach (ViewFamilyType curVFT in collector1)
            {
                if (curVFT.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorPlanVFT = curVFT;
                }
            }

            ViewFamilyType ceilingPlanVFT = null;
            foreach (ViewFamilyType curVFT in collector1)
            {
                if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT = curVFT;
                }
            }

            // get a titleblock type
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
            collector2.WhereElementIsElementType();



            //Loop through the number 1 to the number variable
            for (int i = 1; i <= number; i++)
            {

                // increment the current elevation by the floor height value
                double curElev = startElev + ((i - 1) * floorHeight);

                // create a level for each number
                Level newLevel = Level.Create(doc, curElev);
                newLevel.Name = "LevelTest " + i;

                //If the number is divisible by both 3 and 5, create a sheet and name it "FIZZBUZZ_#"
                if (i % 3 == 0 && i % 5 == 0)
                {
                    // create a sheet
                    ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
                    newSheet.Name = "FIZZBUZZ_ " + i;
                    newSheet.SheetNumber = "Test " + i;

                    // create a floor plan view
                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPlan.Name = "FIZZBUZZ PLAN_" + i;

                    // create a viewport
                    // first create a point
                    XYZ insPoint = new XYZ();
                    XYZ insPoint2 = new XYZ(1, 0.5, 0);
                    Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPlan.Id, insPoint);
                }

                //If the number is divisible by 3, create a floor plan and name it "FIZZ_#"
                else if (i % 3 == 0)
                {
                    // create a floor plan view
                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
                    newFloorPlan.Name = "FIZZ_" + i;
                }

                //If the number is divisible by 5, create a ceiling plan and name it "BUZZ_#"
                else if (i % 5 == 0)
                {
                    // create a ceiling plan view

                    ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
                    newCeilingPlan.Name = "BUZZ_" + i;
                }
            }


            t.Commit();
            t.Dispose();
            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge01";
            string buttonTitle = "Module\r01";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module01,
                Properties.Resources.Module01,
                "Module 01 Challenge");

            return myButtonData.Data;
        }
    }

}