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
    internal class SetParameter : IExternalCommand
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

                if (pickedObj != null)
                {
                    //Retrieve element
                    ElementId eleId = pickedObj.ElementId;
                    Element ele = doc.GetElement(eleId);

                    //Get parameter value
                    Parameter param = ele.get_Parameter(BuiltInParameter.INSTANCE_HEAD_HEIGHT_PARAM);

                    TaskDialog.Show("Parameter values", string.Format("Parameter stoge type {0} and value {1}", param.StorageType.ToString(), param.AsDouble()));

                    //Set parameter value
                    using (Transaction trans = new Transaction(doc, "Set Parameter"))
                    {
                        trans.Start();

                        param.Set(7.5);

                        trans.Commit();
                    }
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
