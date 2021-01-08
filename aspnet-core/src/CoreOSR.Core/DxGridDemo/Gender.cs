using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace CoreOSR.DxGridDemo
{
   public class Gender : Entity<int>
    {
        public string GenderCode { get; set; }
        public string Description { get; set; }
    }
}
