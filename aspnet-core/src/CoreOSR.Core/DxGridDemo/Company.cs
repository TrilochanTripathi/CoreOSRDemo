using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace CoreOSR.DxGridDemo
{
   public class Company : Entity<int>
    {
        public string CompanyCode { get; set; }
        public string Description { get; set; }
    }
}
