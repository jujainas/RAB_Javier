using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAB_Javier
{
    public class AppSkills : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            // 1. Create Tab
            string tabName = "My Tab";
            application.CreateRibbonTab(tabName);

            // 1b. Create tab and panel - safer version
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch (Exception error)
            {
                Debug.Print("Tab already exists. Using existing panel.");
                Debug.Print(error.Message);
            }

            // 2. Create Panel
            string panelName1 = "Test Panel";
            string panelName2 = "Test Panel 2";
            string panelName3 = "Test Panel 3";

            RibbonPanel panel = application.CreateRibbonPanel(tabName, panelName1);
            RibbonPanel panel2 = application.CreateRibbonPanel(panelName2);
            RibbonPanel panel3 = application.CreateRibbonPanel("Architecture", panelName3);

            // 2.a Get existing panel
            List<RibbonPanel> panelList = application.GetRibbonPanels();
            List<RibbonPanel> panelList2 = application.GetRibbonPanels(tabName);

            // 2.b Create/Get panel method - safe method
            RibbonPanel panel4 = CreateGetPanel (application, tabName, panelName1);







            return Result.Succeeded;
        }

        private RibbonPanel CreateGetPanel(UIControlledApplication application, string tabName, string panelName1)
        {
            // look for panel in tab
            foreach (RibbonPanel panel in application.GetRibbonPanels(tabName))
            {
                if (panel.Name == panelName1)
                {
                    return panel;
                }
            }

            //panel not found, so create it
            RibbonPanel returnPanel = application.CreateRibbonPanel(tabName, panelName1);
            return returnPanel;
            //return.app.CreateRibbonPanel(tabName, panelName1); This is the same as what it is above

        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
