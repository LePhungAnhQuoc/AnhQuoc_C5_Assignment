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
    
    public partial class LoanDetailHistory
    {
        public string Id { get; set; }
        public string IdLoanHistory { get; set; }
        public int IdBook { get; set; }
        public System.DateTime ExpDate { get; set; }
        public string Note { get; set; }
        public decimal PaidMoney { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual LoanHistory LoanHistory { get; set; }
    }
}
