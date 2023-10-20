using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace MyRevitCommands
{
    [Transaction(TransactionMode.Manual)]
    internal class PlanView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get document
            Document doc = uidoc.Document;

            //Get family symbol
            ViewFamilyType viewFamily = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewFamilyType))
                .Cast<ViewFamilyType>()
                .First(x => x.ViewFamily == ViewFamily.FloorPlan);

            //Get level
            Level level = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType()                
                .Cast<Level>()
                .First(x => x.Name == "Level 1");

            try
            {
                using (Transaction trans = new Transaction(doc, "Create plan view"))
                {
                    trans.Start();

                    //Create view
                    ViewPlan vPlan = ViewPlan.Create(doc, viewFamily.Id, level.Id);
                    vPlan.Name = "New Floor";

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
