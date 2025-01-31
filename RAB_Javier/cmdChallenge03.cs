using System.Net;
using System.Xml.Linq;

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

            // Your Module 03 Challenge code goes here
            // Delete the TaskDialog below and add your code




            return Result.Succeeded;
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
