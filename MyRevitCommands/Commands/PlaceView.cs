using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRevitCommands
{
    [Transaction(TransactionMode.Manual)]
    internal class PlaceView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get document
            Document doc = uidoc.Document;

            //Find sheet
            ViewSheet vSheet = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Sheets)
                .Cast<ViewSheet>()
                .First(x => x.Name == "My first sheet");

            //Find view
            Element vPlan = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Views)
                .First(x => x.Name == "New Floor");

            //Get midpoint
            BoundingBoxUV outline = vSheet.Outline;
            double xu = (outline.Max.U + outline.Min.U) * 0.5;
            double yu = (outline.Max.V + outline.Min.V) * 0.5;
            XYZ midPoint = new XYZ(xu, yu, 0);

            try
            {
                using (Transaction trans = new Transaction(doc, "Place view"))
                {
                    trans.Start();

                    //Place view
                    Viewport vPort = Viewport.Create(doc, vSheet.Id, vPlan.Id, midPoint);

                    trans.Commit();
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}
