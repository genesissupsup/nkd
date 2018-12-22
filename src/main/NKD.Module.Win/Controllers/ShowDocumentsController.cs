﻿using System;
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

    public partial class ShowDocumentsController : ViewController {

        public ShowDocumentsController() {
            PopupWindowShowAction showDocumentsAction = new PopupWindowShowAction(this, "Show Documents", "View");
            showDocumentsAction.ImageName = "BO_Folder";
            showDocumentsAction.CustomizePopupWindowParams += new CustomizePopupWindowParamsEventHandler(showDocumentsAction_CustomizePopupWindowParams);
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(object);
        }


        void showDocumentsAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e) {

            try
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();

                var o = (System.Data.Entity.Core.Objects.DataClasses.EntityObject)e.Action.SelectionContext.CurrentObject;
                var c = ((DevExpress.ExpressApp.EF.EFObjectSpace)objectSpace).ObjectContext;
                var table = NKD.Module.BusinessObjects.BusinessObjectHelper.GetTableName(c, e.Action.SelectionContext.CurrentObject.GetType());
                var value = (Guid)o.EntityKey.EntityKeyValues[0].Value;

                
                CollectionSource collectionSource = new CollectionSource(objectSpace, typeof(FileData));
                CriteriaOperator c1 = new BinaryOperator(
                    new OperandProperty("TableType"), table,
                    BinaryOperatorType.Equal
                );
                CriteriaOperator c2 = new BinaryOperator(
                    new OperandProperty("ReferenceID"), value,
                    BinaryOperatorType.Equal
                );
                CriteriaOperator co = c1 & c2;
                collectionSource.Criteria.Add("Refences", co);
                //Do this for manual data lists
                //if ((collectionSource.Collection as XPBaseCollection) != null)
                //{
                //    ((XPBaseCollection)collectionSource.Collection).LoadingEnabled = false;
                //}
                ListView view = Application.CreateListView(Application.GetListViewId(typeof(FileData)), collectionSource, false);
                view.Editor.AllowEdit = true;
                foreach (var k in view.AllowNew.GetKeys())
                    view.AllowNew[k] = false;
                foreach (var k in view.AllowDelete.GetKeys())
                    view.AllowDelete[k] = false;
                foreach (var k in view.AllowEdit.GetKeys())
                    view.AllowEdit[k] = false;
                foreach (var k in e.DialogController.AcceptAction.Enabled.GetKeys())
                    e.DialogController.AcceptAction.Enabled[k] = false;
                e.DialogController.AcceptAction.Enabled.SetItemValue("Item.Enabled", false);
                e.View = view;
                e.DialogController.SaveOnAccept = false;
                
            }
            catch (Exception ex)
            { }


        }
    }
   
}