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
    internal class ChangeLocation : IExternalCommand
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

                //Display element id
                if (pickedObj != null)
                {
                    //Retrieve element
                    ElementId eleId = pickedObj.ElementId;
                    Element ele = doc.GetElement(eleId);

                    using (Transaction trans = new Transaction(doc, "Change location"))
                    {
                        trans.Start();

                        //Set location
                        LocationPoint locp = ele.Location as LocationPoint;

                        if (locp != null)
                        {
                            XYZ loc = locp.Point;
                            XYZ newloc = new XYZ(loc.X + 3, loc.Y, loc.Z);

                            locp.Point = newloc;
                            trans.Commit();
                        }                        
                    }
                }

                return Result.Succeeded;
            }
            catch (Exception e )
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}
