﻿using Api.Models.Entities;
using Api.Utilities;

namespace Api.Models.Dtos
{
    public class UpdateAuthorDto : IMap<Author>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime BoF { get; set; }

        public string Summary { get; set; } = null!;

        public bool Status { get; set; }

        public DateTime ModifiedAt { get; set; }


        public void MapFrom(Author entity)
        {
            Utilitys.Copy(this, entity);
        }

        public void MapTo(ref Author entity)
        {
            Utilitys.Copy(entity, this);
        }
    }
}
