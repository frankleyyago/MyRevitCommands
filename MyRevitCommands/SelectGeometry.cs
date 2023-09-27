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
    internal class SelectGeometry : IExternalCommand
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

                    //Get geometry
                    Options gOptions = new Options();
                    gOptions.DetailLevel = ViewDetailLevel.Fine;
                    GeometryElement geom = ele.get_Geometry(gOptions);

                    //Traverse geometry
                    foreach (GeometryObject gObj in geom)
                    {
                        Solid gSolid = gObj as Solid;

                        int faces = 0;
                        double area = 0.0;

                        foreach (Face gFace in gSolid.Faces)
                        {
                            area += gFace.Area;
                            faces++;
                        }

                        area = UnitUtils.ConvertFromInternalUnits(area, UnitTypeId.SquareMeters);

                        TaskDialog.Show("Geometry", string.Format($"Number of faces: {faces}\n Total area: {area}"));
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
