﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
            RibbonPanel panel2 = application.CreateRibbonPanel(tabName,panelName2);
            /*RibbonPanel panel3 = application.CreateRibbonPanel("Architecture", panelName3);
            it does not allow you to create a panel in a tab that are not custom ,
            so the above line will throw an error if the tab is not custom*/

            // 2.a Get existing panel
            List<RibbonPanel> panelList = application.GetRibbonPanels();
            List<RibbonPanel> panelList2 = application.GetRibbonPanels(tabName);

            // 2.b Create/Get panel method - safe method
            RibbonPanel panel4 = CreateGetPanel (application, tabName, panelName1);

            // 3. Create button data
            PushButtonData buttonData1 = new PushButtonData("button1","Command1",
                Assembly.GetExecutingAssembly().Location,"RAB_Javier.Command1");

            PushButtonData buttonData2 = new PushButtonData("button2", "Buttom\rCommand2",
               Assembly.GetExecutingAssembly().Location, "RAB_Javier.Command2");
            // Adding a \r makes the text to be on another line below

            // 4. add tooltips
            buttonData1.ToolTip = "This is Command 1";
            buttonData2.ToolTip = "This is Command 2";

            //5. Add images
            buttonData1.Image = ConvertToImageSource(Properties.Resources.Blue16);
            buttonData1.LargeImage = ConvertToImageSource(Properties.Resources.Blue32);
            buttonData2.Image = ConvertToImageSource(Properties.Resources.Green16);
            buttonData2.LargeImage = ConvertToImageSource(Properties.Resources.Green32);

            //6. Create push buttons
            PushButton pushButton1 = panel.AddItem(buttonData1) as PushButton;
            PushButton pushButton2 = panel2.AddItem(buttonData2) as PushButton;




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
            //return.application.CreateRibbonPanel(tabName, panelName1); This is the same as what it is above

        }

        public BitmapImage ConvertToImageSource(byte[] imageData)
        {
            using (MemoryStream mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = mem;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();

                return bmi;
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
