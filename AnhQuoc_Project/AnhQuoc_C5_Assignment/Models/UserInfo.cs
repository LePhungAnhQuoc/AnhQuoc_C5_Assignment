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
    
    public partial class UserInfo : IMapFromModel
    {
        public string IdUser { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    
        public virtual User User { get; set; }

        public object MapToAdd()
        {
            AddUserInfoDto result = new AddUserInfoDto();
            Utilitys.Copy(result, this);
            return result;
        }

        public object MapToUpdate()
        {
            UpdateUserInfoDto result = new UpdateUserInfoDto();
            Utilitys.Copy(result, this);
            return result;
        }
    }
}
