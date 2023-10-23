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
    internal class ViewFilter : IExternalCommand
    {
        [Obsolete]
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get document
            Document doc = uidoc.Document;

            //Create filter
            List<ElementId> cats = new List<ElementId>();
            cats.Add(new ElementId(BuiltInCategory.OST_Sections));

            ElementParameterFilter filter = new ElementParameterFilter
                (ParameterFilterRuleFactory.CreateContainsRule(
                    new ElementId(BuiltInParameter.VIEW_NAME), "WIP", false));

            try
            {
                using (Transaction trans = new Transaction(doc, "Apply filter"))
                {
                    trans.Start();

                    //Apply filter
                    ParameterFilterElement filterElement = ParameterFilterElement.Create(doc, "My First Filter", cats, filter);
                    doc.ActiveView.AddFilter(filterElement.Id);
                    doc.ActiveView.SetFilterVisibility(filterElement.Id, false);


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
