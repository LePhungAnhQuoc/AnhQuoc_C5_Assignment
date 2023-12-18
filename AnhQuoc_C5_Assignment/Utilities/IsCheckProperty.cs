﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class IsCheckProperties
    {
        public bool IsExept { get; set; }
        public List<string> ListProperties { get; set; }

        public IsCheckProperties(params string[] listProperties)
        {
            IsExept = false;
            ListProperties = new List<string>(listProperties);
        }
        public IsCheckProperties(bool isExept, params string[] listProperties) : this(listProperties)
        {
            IsExept = isExept;
        }
    }
}
