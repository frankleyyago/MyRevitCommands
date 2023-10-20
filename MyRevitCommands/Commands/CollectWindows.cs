using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace MyRevitCommands
{
    [Transaction(TransactionMode.Manual)]
    internal class CollectWindows : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get Document
            Document doc = uidoc.Document;

            //Create filtered element collector
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            //Create filter
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Windows);

            //Apply filter
            IList<Element> windows = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();

            TaskDialog.Show("Windows", string.Format("{0} windows counted!", windows.Count));

            return Result.Succeeded;
        }
    }
}
