//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManager.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tasks
    {
        public int TaskID { get; set; }
        public Nullable<int> ParentTaskID { get; set; }
        public int ProjectID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
    
        public virtual ParentTasks ParentTasks { get; set; }
        public virtual Projects Projects { get; set; }
    }
}
