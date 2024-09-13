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
    using AnhQuoc_C5_Assignment.DTOs.ApiDtos;
    using Api.Models.Dtos;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LoanSlip : IMapFromModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanSlip()
        {
            this.LoanDetails = new HashSet<LoanDetail>();
        }
        [Key]
        public string Id { get; set; }
        public string IdUser { get; set; }
        public string IdReader { get; set; }
        public int Quantity { get; set; }
        public System.DateTime LoanDate { get; set; }
        public System.DateTime ExpDate { get; set; }
        public decimal LoanPaid { get; set; }
        public decimal Deposit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanDetail> LoanDetails { get; set; }
        public virtual Reader Reader { get; set; }
        public virtual User User { get; set; }

        public object MapToAdd()
        {
            AddLoanSlipDto result = new AddLoanSlipDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateLoanSlipDto result = new UpdateLoanSlipDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
