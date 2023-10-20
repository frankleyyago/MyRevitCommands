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
    [Transaction(TransactionMode.ReadOnly)]
    internal class GetParameter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get document
            Document doc = uidoc.Document;

            try
            {
                //Pick object
                Reference pickedObj = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

                if (pickedObj != null )
                {
                    //Retrieve element
                    ElementId eleId = pickedObj.ElementId;
                    Element ele = doc.GetElement(eleId);

                    //Get parameter
                    Parameter param = ele.LookupParameter("Head Height");
                    InternalDefinition paramDef = param.Definition as InternalDefinition;

                    TaskDialog.Show("Parameters", string.Format("Name: {0}\n Id: {1}\n BuiltInParameter: {2}", paramDef.Name, paramDef.Id, paramDef.BuiltInParameter));
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
