using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace CoreOSR.DxGridDemo
{
    public class JobTitle:Entity<int>
    {
        public string JobCode { get; set; }
        public string Description { get; set; }
    }
}
