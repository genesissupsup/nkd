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
    public class UpdateConnectionStringController : ViewController
    {

        public UpdateConnectionStringController()
        {

        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var newController = Frame.GetController<NewObjectViewController>();
            if (newController != null)
            {
                var myAction = new SimpleAction(this, "UpdateConnectionString" + this.View.ObjectTypeInfo.Type.AssemblyQualifiedName, DevExpress.Persistent.Base.PredefinedCategory.Tools);
                myAction.Execute += myAction_Execute;
                myAction.Caption = "Update Connection String";
                myAction.ImageName = "Action_ResetPassword";
                newController.Actions.Add(myAction);
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var f = new NKD.Module.Win.Controllers.UpdateConnectionString(Application);
            f.ShowDialog();
        }
    }

    //public class UpdateConnectionStringController : NewObjectViewController
    //{

    //    public UpdateConnectionStringController()
    //    {
    //        var myAction = new SimpleAction(this, "UpdateConnectionString" + this.View.ObjectTypeInfo.Type.AssemblyQualifiedName, DevExpress.Persistent.Base.PredefinedCategory.Tools);
    //        myAction.Execute += myAction_Execute;
    //        myAction.Caption = "Update Connection String";
    //        myAction.ImageName = "Action_ResetPassword";
    //        Actions.Add(myAction);

    //    }

    //    void myAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
    //    {
    //        var f = new NKD.Module.Win.Controllers.UpdateConnectionString(Application);
    //        f.ShowDialog();
    //    }
    //}
}
