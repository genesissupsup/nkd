//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NKD.Import.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class X_PrivateData
    {
        public System.Guid PrivateDataID { get; set; }
        public string UniqueID { get; set; }
        public string UniqueIDSystemDataType { get; set; }
        public string TableType { get; set; }
        public Nullable<System.Guid> ReferenceID { get; set; }
        public string UserDataType { get; set; }
        public string Value { get; set; }
        public string SystemDataType { get; set; }
        public Nullable<bool> IsReadOnly { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public int Version { get; set; }
        public Nullable<System.Guid> VersionAntecedentID { get; set; }
        public Nullable<int> VersionCertainty { get; set; }
        public Nullable<System.Guid> VersionWorkflowInstanceID { get; set; }
        public Nullable<System.Guid> VersionUpdatedBy { get; set; }
        public Nullable<System.Guid> VersionDeletedBy { get; set; }
        public Nullable<System.Guid> VersionOwnerContactID { get; set; }
        public Nullable<System.Guid> VersionOwnerCompanyID { get; set; }
        public Nullable<System.DateTime> VersionUpdated { get; set; }
    }
}
