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
    [Transaction(TransactionMode.ReadOnly)]
    internal class ProjectRay : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get document
            Document doc = uidoc.Document;

            try
            {
                //Pick an element
                Reference pickedObj = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

                if (pickedObj != null)
                {
                    //Get element id
                    ElementId eleId = pickedObj.ElementId;

                    //Get element by id
                    Element ele = doc.GetElement(eleId);

                    //Project ray
                    LocationPoint locP = ele.Location as LocationPoint;
                    XYZ p1 = locP.Point;

                    //Ray
                    XYZ rayd = new XYZ(0, 0, 1);

                    ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Roofs);
                    ReferenceIntersector refI = new ReferenceIntersector(filter, FindReferenceTarget.Face, (View3D)doc.ActiveView);
                    ReferenceWithContext refC = refI.FindNearest(p1, rayd);
                    Reference reference = refC.GetReference();
                    XYZ intPoint = reference.GlobalPoint;
                    double dist = p1.DistanceTo(intPoint);

                    TaskDialog.Show("Ray", string.Format($"Distance to roof {dist}"));
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
