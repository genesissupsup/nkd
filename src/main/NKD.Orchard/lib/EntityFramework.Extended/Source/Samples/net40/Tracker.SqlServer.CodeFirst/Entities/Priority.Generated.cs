﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Tracker.SqlServer.CodeFirst.Entities
{
    public partial class Priority
    {
        public Priority()
        {
            Tasks = new List<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.Byte[] RowVersion { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}