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
    [Transaction(TransactionMode.ReadOnly)]
    public class GetElementId : IExternalCommand
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

                //Retrieve element
                ElementId eleId = pickedObj.ElementId;

                Element ele = doc.GetElement(eleId);

                //Get element type
                ElementId eTypeId = ele.GetTypeId();
                ElementType eType = doc.GetElement(eTypeId) as ElementType;

                //Display element id
                if (pickedObj != null)
                {
                    TaskDialog.Show("Element classification", $"ID: {eleId.ToString()}" +
                        $"\nCategory: {ele.Category.Name}" +
                        $"\nInstance: {ele.Name}" +
                        $"\nSymbol: {eType.Name}" +
                        $"\nFamily: {eType.FamilyName} ");
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
