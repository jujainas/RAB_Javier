using System.Net;
using System.Xml.Linq;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;

namespace RAB_Javier
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge03 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            int totalCounter = 0;

            // 3. define the class data
            List<movingList> furnitureList = GetRoomFurniture();

            // 4. get all the rooms
            FilteredElementCollector roomCollector = new FilteredElementCollector(doc);
            roomCollector.OfCategory(BuiltInCategory.OST_Rooms);

            // 5. loop through the rooms and insert furniture
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Move in");
                foreach (Room curRoom in roomCollector)
                {
                    int roomCounter = 0;

                    // 5.a get room data
                    string roomName = curRoom.Name;
                    LocationPoint roomLoc = curRoom.Location as LocationPoint;
                    XYZ roomPoint = roomLoc.Point;

                    // 5.b loop through furnitureList and find matching room
                    foreach (movingList curRoomFur in furnitureList)
                    {
                        if(roomName.Contains(curRoomFur.RoomName))
                        {
                            // 5.c get family symbol and activate
                            FamilySymbol curFS = GetFamilySymbol(doc, curRoomFur.FamilyName, curRoomFur.FamilyType);

                            // 5.d check for null
                            if (curFS == null)
                            {
                                TaskDialog.Show("Error", "Could not find specified family. Check your family data.");
                                continue;
                            }

                            curFS.Activate();

                            // 5.e loop through quantity number and insert families
                            for (int i = 1; i <= curRoomFur.Quantity; i++)
                            {
                                FamilyInstance curFI = doc.Create.NewFamilyInstance(roomPoint, curFS, StructuralType.NonStructural);
                                roomCounter++;
                                totalCounter++;
                            }
                        }
                    }

                    // 6. Update furniture count for room
                    Parameter roomCount = curRoom.LookupParameter("Furniture Count");

                    if (roomCount != null)
                    {
                        roomCount.Set(roomCounter);
                    }
                }

                t.Commit();
            }

            // 7. alert user
            TaskDialog.Show("Complete", $"You moved {totalCounter} pieces of furniture in the building. Great work!");

            return Result.Succeeded;
        }

        private FamilySymbol GetFamilySymbol(Document doc, string familyName, string familyType)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilySymbol));

            foreach (FamilySymbol curFS in collector)
            {
                if (curFS.FamilyName == familyName && curFS.Name == familyType)
                    return curFS;
            }
            return null;
        }

        // 3. Define a method 
        private List<movingList> GetRoomFurniture()
        {
            // Create a list to hold movingList instances
            List<movingList> furnitureList = new List<movingList>();

            // Add instances
            furnitureList.Add(new movingList("Classroom", "Desk", "Teacher", 1));
            furnitureList.Add(new movingList("Classroom", "Desk", "Student", 6));
            furnitureList.Add(new movingList("Classroom", "Chair-Desk", "Default", 7));
            furnitureList.Add(new movingList("Classroom", "Shelf", "Large", 1));
            furnitureList.Add(new movingList("Office", "Desk", "Teacher", 1));
            furnitureList.Add(new movingList("Office", "Chair-Executive", "Default", 1));
            furnitureList.Add(new movingList("Office", "Shelf", "Small", 1));
            furnitureList.Add(new movingList("Office", "Chair-Task", "Default", 1));
            furnitureList.Add(new movingList("VR Lab", "Table-Rectangular", "Small", 1));
            furnitureList.Add(new movingList("VR Lab", "Table-Rectangular", "Large", 8));
            furnitureList.Add(new movingList("VR Lab", "Chair-Task", "Default", 9));
            furnitureList.Add(new movingList("Computer Lab", "Desk", "Teacher", 1));
            furnitureList.Add(new movingList("Computer Lab", "Desk", "Student", 10));
            furnitureList.Add(new movingList("Computer Lab", "Chair-Desk", "Default", 11));
            furnitureList.Add(new movingList("Computer Lab", "Shelf", "Large", 2));
            furnitureList.Add(new movingList("Student Lounge", "Sofa", "Large", 2));
            furnitureList.Add(new movingList("Student Lounge", "Table-Coffee", "Square", 2));
            furnitureList.Add(new movingList("Teacher Lounge", "Sofa", "Small", 2));
            furnitureList.Add(new movingList("Teacher Lounge", "Table-Coffee", "Large", 1));
            furnitureList.Add(new movingList("Waiting", "Chair-Waiting", "Default", 2));
            furnitureList.Add(new movingList("Waiting", "Table-Coffee", "Large", 1));

            return furnitureList;
        }

        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge03";
            string buttonTitle = "Module\r03";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module03,
                Properties.Resources.Module03,
                "Module 03 Challenge");

            return myButtonData.Data;
        }
    }


    //1. Create class
    public class movingList
    {
        public string RoomName { get; set; }
        public string FamilyName { get; set; }
        public string FamilyType { get; set; }
        public int Quantity { get; set; }

        //2. add constructor to class
        public movingList(string _roomName, string _familyName, string _familytype, int _quantity)

        {
            RoomName = _roomName;
            FamilyName = _familyName;
            FamilyType = _familytype;
            Quantity = _quantity;
        }
    }
}
