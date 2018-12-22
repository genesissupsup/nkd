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
    public class SendConfigController : ViewController
    {
        public SendConfigController()
        {
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var newController = Frame.GetController<NewObjectViewController>();
            if (newController != null)
            {
                var myAction = new SimpleAction(this, "ShareConfiguration" + this.View.ObjectTypeInfo.Type.AssemblyQualifiedName, DevExpress.Persistent.Base.PredefinedCategory.Tools);
                myAction.Execute += myAction_Execute;
                myAction.ImageName = "BO_Contact";
                myAction.Caption = "Share Configuration";
                newController.Actions.Add(myAction);
            }
        }

        void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var f = new NKD.Module.Win.Controllers.SendConfig(Application);
            f.ShowDialog();
        }
    }

    //public class SendConfigController : NewObjectViewController
    //{
    //    public SendConfigController()
    //    {
    //        var myAction = new SimpleAction(this, "Share Configuration", DevExpress.Persistent.Base.PredefinedCategory.Tools);
    //        myAction.Execute += myAction_Execute;
    //        myAction.ImageName = "BO_Contact";
    //        Actions.Add(myAction);
    //    }

    //    void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
    //    {
    //        var f = new NKD.Module.Win.Controllers.SendConfig(Application);
    //        f.ShowDialog();
    //    }
    //}
}
