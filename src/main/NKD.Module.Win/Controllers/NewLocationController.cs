using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using NKD.Module.BusinessObjects;
using DevExpress.ExpressApp.SystemModule;
//using DevExpress.Persistent.BaseImpl;
namespace NKD.Module.Win.Controllers
{

    public partial class NewLocationController : ViewController
    {

        public NewLocationController()
        {
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(object);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var newController = Frame.GetController<NewObjectViewController>();
            var myAction = new SimpleAction(this, "Add Location", DevExpress.Persistent.Base.PredefinedCategory.View);
            myAction.TargetObjectType = typeof(object);
            myAction.Execute += (s, e) =>
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(LocationData));
                LocationData newLocation = objectSpace.CreateObject<LocationData>();
                DetailView detailView = Application.CreateDetailView(objectSpace, newLocation);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.ShowViewParameters.CreatedView = detailView;
            };

            myAction.ImageName = "BO_Country_v92";
            newController.Actions.Add(myAction);
            newController.ObjectCreated += (s, e) =>
            {
                if (e.CreatedObject is LocationData)
                {
                    try
                    {

                        var view = s as DevExpress.ExpressApp.ViewController;
                        if (view == null || view.View == null || view.View.SelectedObjects == null || view.View.SelectedObjects.Count < 1)
                            return;
                        if (view.View.SelectedObjects[0] is System.Data.Entity.Core.Objects.DataClasses.EntityObject)
                        {
                            var o = (System.Data.Entity.Core.Objects.DataClasses.EntityObject)view.View.SelectedObjects[0];
                            var c = ((DevExpress.ExpressApp.EF.EFObjectSpace)view.View.ObjectSpace).ObjectContext;
                            //var t = c.MetadataWorkspace.GetEntityContainer(c.DefaultContainerName, System.Data.Metadata.Edm.DataSpace.CSpace);
                            ((LocationData)e.CreatedObject).TableType = NKD.Module.BusinessObjects.BusinessObjectHelper.GetTableName(c, view.View.SelectedObjects[0].GetType());
                            ((LocationData)e.CreatedObject).ReferenceID = (Guid)o.EntityKey.EntityKeyValues[0].Value;
                            ((LocationData)e.CreatedObject).LocationDataID = Guid.NewGuid();

                        }
                        else if (view.View.SelectedObjects[0] is DevExpress.Xpo.XPLiteObject)
                        {
                            XPLiteObject o = view.View.SelectedObjects[0] as XPLiteObject;
                            ((LocationData)e.CreatedObject).ReferenceID = (Guid)o.This.GetType().GetProperty(o.ClassInfo.KeyProperty.Name).GetValue(o.This);
                            ((LocationData)e.CreatedObject).TableType = o.ClassInfo.TableName;
                        }
                    }
                    catch { }
                }
            };
        }

    }

}