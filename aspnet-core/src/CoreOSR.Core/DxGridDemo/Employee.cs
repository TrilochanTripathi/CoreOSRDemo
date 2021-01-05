using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Abp.Domain.Entities;

namespace CoreOSR.DxGridDemo
{
    [Table("Employee")]
   public class Employee:Entity<int>
    {
        [Required]
        public string Name { get; set; }
        [MaxLength(5)]
        public string GenderId { get; set; }
        public DateTime DOB { get; set; }
        public string JobTitleId { get; set; }
        public string CompanyId { get; set; }
        public double Salary { get; set; }
        public bool IsAvailable { get; set; }

    }
}
