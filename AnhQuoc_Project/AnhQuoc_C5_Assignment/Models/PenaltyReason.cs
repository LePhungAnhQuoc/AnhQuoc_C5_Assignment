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
    
    public partial class PenaltyReason : IMapFromModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }

        public object MapToAdd()
        {
            AddPenaltyReasonDto result = new AddPenaltyReasonDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdatePenaltyReasonDto result = new UpdatePenaltyReasonDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
