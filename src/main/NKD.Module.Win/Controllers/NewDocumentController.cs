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

namespace NKD.Module.Win.Controllers
{
    public partial class NewDocumentController : ViewController
    {
        public NewDocumentController()
        {
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(object);
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            var newController = Frame.GetController<NewObjectViewController>();

            var myAction = new SimpleAction(this, "Add Document", DevExpress.Persistent.Base.PredefinedCategory.View);
            myAction.TargetObjectType = typeof(object);
            myAction.Execute += (s, e) =>
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(FileData));
                FileData newFile = objectSpace.CreateObject<FileData>();
                DetailView detailView = Application.CreateDetailView(objectSpace, newFile);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.ShowViewParameters.CreatedView = detailView;
            };
            myAction.ImageName = "BO_FileAttachment";
            newController.Actions.Add(myAction);
            newController.ObjectCreated += (s, e) =>
            {
                if (e.CreatedObject is FileData)
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
                            ((FileData)e.CreatedObject).TableType = NKD.Module.BusinessObjects.BusinessObjectHelper.GetTableName(c, view.View.SelectedObjects[0].GetType());
                            ((FileData)e.CreatedObject).ReferenceID = (Guid)o.EntityKey.EntityKeyValues[0].Value;
                            ((FileData)e.CreatedObject).FileDataID = Guid.NewGuid();

                        }
                        else if (view.View.SelectedObjects[0] is DevExpress.Xpo.XPLiteObject)
                        {
                            XPLiteObject o = view.View.SelectedObjects[0] as XPLiteObject;
                            ((FileData)e.CreatedObject).ReferenceID = (Guid)o.This.GetType().GetProperty(o.ClassInfo.KeyProperty.Name).GetValue(o.This);
                            ((FileData)e.CreatedObject).TableType = o.ClassInfo.TableName;
                        }
                    }
                    catch { }
                }
            };
        }

    }

}