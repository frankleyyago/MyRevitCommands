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
    internal class PlaceFamily : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get document
            Document doc = uidoc.Document;

            //Get family symbol
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FamilySymbol symbol = collector.OfClass(typeof(FamilySymbol))
                .WhereElementIsElementType()
                .Cast<FamilySymbol>()
                .First(x => x.Name == "60\" x 30\"");

            //Create a transaction
            try
            {
                using (Transaction trans = new Transaction(doc, "Place family"))
                {
                    trans.Start();

                    if (!symbol.IsActive)
                    {
                        symbol.Activate();
                    }

                    doc.Create.NewFamilyInstance(new XYZ(0, 0, 0), symbol, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

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
