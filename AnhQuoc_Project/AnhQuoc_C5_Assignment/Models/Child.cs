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
    
    public partial class Child : IMapFromModel
    {
        public string IdReader { get; set; }
        public string IdAdult { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
    
        public virtual Adult Adult { get; set; }
        public virtual Reader Reader { get; set; }

        public object MapToAdd()
        {
            AddChildDto result = new AddChildDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateChildDto result = new UpdateChildDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
