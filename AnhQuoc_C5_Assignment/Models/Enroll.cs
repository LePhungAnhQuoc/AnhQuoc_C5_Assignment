//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnhQuoc_C5_Assignment
{
    using System;
    using System.Collections.Generic;
    
    public partial class Enroll
    {
        public string Id { get; set; }
        public string ISBN { get; set; }
        public string IdReader { get; set; }
        public System.DateTime EnrolDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Note { get; set; }
        public Nullable<int> IdBook { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual BookISBN BookISBN { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
