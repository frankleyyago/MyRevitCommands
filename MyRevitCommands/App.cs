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
            //Create a button
            PushButtonData button = new PushButtonData("Button1", "PlaceFamily", path, "MyRevitCommands.PlaceFamily");

            //Create a panel
            RibbonPanel panel = application.CreateRibbonPanel("Commands", "Commands");

            //Retrieve the path of the icon image
            Uri imagePath = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "Icon.ico"));
            BitmapImage image = new BitmapImage(imagePath);

            //Add the button to the panel
            PushButton pushButton = panel.AddItem(button) as PushButton;
            pushButton.LargeImage = image;

            return Result.Succeeded;
        }
    }
}
