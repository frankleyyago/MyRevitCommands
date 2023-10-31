using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;

namespace MyRevitCommands
{
    internal class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //Create a ribbon tab
            application.CreateRibbonTab("Commands");

            //Retrieve the path of the current executing assembly
            string path = Assembly.GetExecutingAssembly().Location;
            //Create buttons
            PushButtonData button1 = new PushButtonData("Button1", "ChangeLocation", path, "MyRevitCommands.ChangeLocation");
            PushButtonData button2 = new PushButtonData("Button2", "CollectWindows", path, "MyRevitCommands.CollectWindows");
            PushButtonData button3 = new PushButtonData("Button16", "CreateSheet", path, "MyRevitCommands.CreateSheet");
            PushButtonData button4 = new PushButtonData("Button3", "DeleteElement", path, "MyRevitCommands.DeleteElement");
            PushButtonData button5 = new PushButtonData("Button4", "EditElement", path, "MyRevitCommands.EditElement");
            PushButtonData button6 = new PushButtonData("Button5", "ElementIntersection", path, "MyRevitCommands.ElementIntersection");
            PushButtonData button7 = new PushButtonData("Button6", "GetElementId", path, "MyRevitCommands.GetElementId");
            PushButtonData button8 = new PushButtonData("Button7", "GetParameter", path, "MyRevitCommands.GetParameter");
            PushButtonData button9 = new PushButtonData("Button8", "PlaceFamily", path, "MyRevitCommands.PlaceFamily");
            PushButtonData button10 = new PushButtonData("Button9", "PlaceLineElement", path, "MyRevitCommands.PlaceLineElement");
            PushButtonData button11 = new PushButtonData("Button10", "PlaceLoopElement", path, "MyRevitCommands.PlaceLoopElement");
            PushButtonData button12 = new PushButtonData("Button11", "PlaceView", path, "MyRevitCommands.PlaceView");
            PushButtonData button13 = new PushButtonData("Button12", "PlanView", path, "MyRevitCommands.PlanView");
            PushButtonData button14 = new PushButtonData("Button13", "ProjectRay", path, "MyRevitCommands.ProjectRay");
            PushButtonData button15 = new PushButtonData("Button14", "SelectGeometry", path, "MyRevitCommands.SelectGeometry");
            PushButtonData button16 = new PushButtonData("Button15", "SetParameter", path, "MyRevitCommands.SetParameter");
            PushButtonData button17 = new PushButtonData("Button17", "TagView", path, "MyRevitCommands.TagView");
            PushButtonData button18 = new PushButtonData("Button18", "ViewFilter", path, "MyRevitCommands.ViewFilter");

            //Create a panel
            RibbonPanel panel1 = application.CreateRibbonPanel("Commands", "Retrieve information");
            panel1.AddSeparator();
            RibbonPanel panel2 = application.CreateRibbonPanel("Commands", "Create Element");
            panel1.AddSeparator();
            RibbonPanel panel3 = application.CreateRibbonPanel("Commands", "Change Element");
            panel1.AddSeparator();
            RibbonPanel panel4 = application.CreateRibbonPanel("Commands", "Documentation");

            //Retrieve the path of the icon image
            Uri imagePath = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "Icon.ico"));
            BitmapImage image = new BitmapImage(imagePath);

            //Add the button to the panel
            PushButton pushButton1 = panel3.AddItem(button1) as PushButton;
            PushButton pushButton2 = panel1.AddItem(button2) as PushButton;
            PushButton pushButton3 = panel4.AddItem(button3) as PushButton;
            PushButton pushButton4 = panel2.AddItem(button4) as PushButton;
            PushButton pushButton5 = panel3.AddItem(button5) as PushButton;
            PushButton pushButton6 = panel3.AddItem(button6) as PushButton;
            PushButton pushButton7 = panel1.AddItem(button7) as PushButton;
            PushButton pushButton8 = panel1.AddItem(button8) as PushButton;
            PushButton pushButton9 = panel2.AddItem(button9) as PushButton;
            PushButton pushButton10 = panel2.AddItem(button10) as PushButton;
            PushButton pushButton11 = panel2.AddItem(button11) as PushButton;
            PushButton pushButton12 = panel4.AddItem(button12) as PushButton;
            PushButton pushButton13 = panel4.AddItem(button13) as PushButton;
            PushButton pushButton14 = panel1.AddItem(button14) as PushButton;
            PushButton pushButton15 = panel1.AddItem(button15) as PushButton;
            PushButton pushButton16 = panel3.AddItem(button16) as PushButton;
            PushButton pushButton17 = panel4.AddItem(button17) as PushButton;
            PushButton pushButton18 = panel4.AddItem(button18) as PushButton;

            //Add image to the button
            pushButton1.LargeImage = image;
            pushButton2.LargeImage = image;
            pushButton3.LargeImage = image;
            pushButton4.LargeImage = image;
            pushButton5.LargeImage = image;
            pushButton6.LargeImage = image;
            pushButton7.LargeImage = image;
            pushButton8.LargeImage = image;
            pushButton9.LargeImage = image;
            pushButton10.LargeImage = image;
            pushButton11.LargeImage = image;
            pushButton12.LargeImage = image;
            pushButton13.LargeImage = image;
            pushButton14.LargeImage = image;
            pushButton15.LargeImage = image;
            pushButton16.LargeImage = image;
            pushButton17.LargeImage = image;
            pushButton18.LargeImage = image;

            return Result.Succeeded;
        }
    }
}
