using Autodesk.Revit.DB.Architecture;

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
            if(roomName.Contains("Office"))
            {
                TaskDialog.Show("Test", "Found the room!");
            }


            return Result.Succeeded;
        }
    }

    

}