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



            return Result.Succeeded;
        }
    }

    // 1. Define a class
    public class Building
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfFloors { get; set; }
        public double Area { get; set; }

        //3. add constructor to class
        public Building(string _name, string _address, int _numberOfFloors, double _area)

        {
            Name = _name;
            Address = _address;
            NumberOfFloors = _numberOfFloors;
            Area = _area;
        }

    }

}