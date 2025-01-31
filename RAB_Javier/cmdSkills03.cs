using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using System.Runtime.CompilerServices;

namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills03 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // 2. Create instance of class
            Building building1 = new Building("Big Office Building", "10 Main Street", 10, 150000);
            Building building2 = new Building("Fancy Hotel", "15 Main Street", 15, 200000);

            building1.NumberOfFloors = 11;

            List<Building> buildings = new List<Building>();
            buildings.Add(building1);
            buildings.Add(building2);
            buildings.Add(new Building("Hospital", "20 Main Street", 20, 350000));
            buildings.Add(new Building("Giant Store", "30 Main Street", 5, 400000));

            // 5. create neighborhood instance
            Neighborhood downtown = new Neighborhood("Downtown", "Middletown", "CT", buildings);

            TaskDialog.Show("Test", $"There are {downtown.GetBuildingCount()} buildings in the " + $"{downtown.Name} neighborhood.");

            // 6. work with rooms
            FilteredElementCollector roomCollector = new FilteredElementCollector(doc);
            roomCollector.OfCategory(BuiltInCategory.OST_Rooms);
            Room curRoom = roomCollector.First() as Room;

            // 7. get room name
            string roomName = curRoom.Name;

            // 7.a check room name
            if (roomName.Contains("Office"))
            {
                TaskDialog.Show("Test", "Found the room!");
            }

            // 7.b get room point
            Location roomLocation = curRoom.Location;
            LocationPoint roomLocPt = roomLocation as LocationPoint;
            XYZ roomPoint = roomLocPt.Point;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Insert families into rooms");
                // 8. insert families
                FamilySymbol curFamSymbol = GetFamilySymbolByName(doc, "Desk", "Large");
                curFamSymbol.Activate(); //this line of code has to be inside the transaction

                foreach (Room curRoom2 in roomCollector)
                {
                    LocationPoint loc = curRoom2.Location as LocationPoint;

                    FamilyInstance curFamInstance = doc.Create.NewFamilyInstance(loc.Point, curFamSymbol, StructuralType.NonStructural);

                    string department = GetParameterValueAsString(curRoom2, "Department");
                    double area = GetParameterValueAsDouble (curRoom2, BuiltInParameter.ROOM_AREA);
                    double area2 = GetParameterValueAsDouble(curRoom, "Area");

                    SetParameterValue(curRoom2, "Department", "Architecture");
                }
                t.Commit();
            }
            return Result.Succeeded;
        }
        private void SetParameterValue (Element curElem, string paramName, string value)
        {
            Parameter curParam = curElem.LookupParameter(paramName);

            if (curParam != null)
            {
               curParam.Set(value);
            }
            
        }

        private void SetParameterValue(Element curElem, string paramName, int value)
        {
            Parameter curParam = curElem.LookupParameter(paramName);

            if (curParam != null)
            {
                curParam.Set(value);
            }
        }
        private string GetParameterValueAsString(Element curElem, string paramName)
        {
            Parameter curParam = curElem.LookupParameter (paramName);
            if (curParam != null)
            {
                return curParam.AsString();
            }
            else
                return "";
        }

        private string GetParameterValueAsString(Element curElem, BuiltInParameter bip)
        {
            Parameter curParam = curElem.get_Parameter(bip);
            if (curParam != null)
            {
                return curParam.AsString();
            }
            else
                return "";
        }

        private double GetParameterValueAsDouble(Element curElem, string paramName)
        {
            Parameter curParam = curElem.LookupParameter(paramName);
            if (curParam != null)
            {
                return curParam.AsDouble();
            }
            else
                return 0;
        }

        private double GetParameterValueAsDouble(Element curElem, BuiltInParameter bip)
        {
            Parameter curParam = curElem.get_Parameter(bip);
            if (curParam != null)
            {
                return curParam.AsDouble();
            }
            else
                return 0;
        }
        private FamilySymbol GetFamilySymbolByName(Document doc, string familyName, string familySymbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector (doc);
            collector.OfClass(typeof(FamilySymbol));

            foreach (FamilySymbol curSymbol in collector)
            {
                if (curSymbol.FamilyName == familyName)
                {
                    if (curSymbol.Name == familySymbolName)
                    {
                        return curSymbol;
                    }
                }

            }

            return null;

        }
    }

    

}