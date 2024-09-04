﻿using Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class AdultRepository : Repository<Adult>
    {
        public AdultRepository(APIProvider<Adult> ApiProvider) : base(ApiProvider)
        {
        }
    }
}
