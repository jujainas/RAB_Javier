namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Let´s create some variables
            // DataType VariableName = Value; <- always end the line with semicolon

            // create string variables
            string text1 = "this is my text";
            string text2 = "this is my next text";

            // combine strings
            string text3 = text1 + text2;
            string text4 = text1 + " " + text2;

            // create number variables
            int number1 = 10;
            double number2 = 20.5;

            // do some maths
            double number3 = number1 + number2;
            double number4 = number1 - number2;
            double number5 = number4 / number3;
            double number6 = number5 * number4;

            // convert meters to feet
            double meters = 4;
            double metersToFeet = meters * 3.28084;

            //convert mm to feet
            double mm = 3500;
            double mmToFeet = mm / 304.8;
            double mmToFeet2 = (mm / 1000) * 3.28084;

            // find the remainder when dividing (ie. the module or mod)
            double remainder1 = 100 % 10; // equals 0 (as 100/10=10 with remainder = 0)
            double remainder2 = 100 % 9; // equals 1 (as 100/9 = 11 with remainder = 1)

            // increment a number by 1
            number6++;
            number6--;

            // use conditionals logic to compare things
            // compare using booleans operators
            // == equals
            // != not equal
            // > greater than
            // < less than
            // >= and <= 

            // check a value and perform a single action if true
            if (number6 > 10)
            {
                //do something if true
            }

            // check a value and perform an action if true and another action if false
            if (number5 == 100)
            {
                // do something if true
            }
            else
            {
                // do something is false
            }

            // check multiple values and perform if true and false
            if (number6 > 10)
            {
                // do something if true
            }
            else if (number6 == 8)
            {
                // do something else if true
            }
            else if (number6 == 6)
            {
                // do something else if true
            }
            else
            {
                // do a third thing
            }

            /* compound conditional estatements
             * look for two things or more using && */
            if (number6 > 10 && number5 == 100)
            {
                // do something if both are true
            }

            // look for either thing using ||
            if (number6 > 10 || number5 == 100)
            {
                // do something if either is true
            }


            // create a list
            List<string> list1 = new List<string>();

            // add items to the list
            list1.Add(text1);
            list1.Add(text2);
            list1.Add("this is new text for my list");

            List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };
            List<string> list3 = new List<string> { "one", "two", "three", "four" };

            // loop through items in the list using a foreach loop

            foreach (string currentstring in list1)
            {
                // do something with current string
            }

            int letterCount = 0;
            foreach (string currentstring in list1)
            {
                letterCount = letterCount + currentstring.Length;
            }

            // loop through a range of numbers
            for (int i = 0; i <= 10; i++)
            {
                // do something
            }

            // loop through a range of variable numbers
            int numberCount = 0;
            int counter = 100;
            for (int i = 0; i <= counter; i++)
            {
                numberCount += i;
            }


            // Your Module 01 Skills code goes here
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Skills", "Got Here!");


            // create a transaction to lock the model
            Transaction t = new Transaction(doc);
            t.Start("I am doing something in Revit");

            // create a floor level
            Level newLevel = Level.Create(doc, 114.8294);
            newLevel.Name = "Level 10 FFL";

            // create a filtered element collector to get view family type

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

            // create a floor plan view


            ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
            newFloorPlan.Name = "Level 10";


            ViewFamilyType ceilingPlanVFT = null;
            foreach (ViewFamilyType curVFT in collector1)
            {
                if (curVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT = curVFT;
                }
            }

            // create a ceiling plan view

            ViewPlan newCeilingPlan = ViewPlan.Create(doc, ceilingPlanVFT.Id, newLevel.Id);
            newCeilingPlan.Name = "Level 10";

            // get a titleblock type
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
            collector2.WhereElementIsElementType();

            // create a sheet
            ViewSheet newSheet = ViewSheet.Create(doc, collector2.FirstElementId());
            newSheet.Name = "My new sheet";
            newSheet.SheetNumber = "A101";

            // create a viewport
            // first create a point
            XYZ insPoint = new XYZ();
            XYZ insPoint2 = new XYZ(1, 0.5, 0);
            Viewport newViewport = Viewport.Create(doc, newSheet.Id, newFloorPlan.Id, insPoint);

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
    }

}
