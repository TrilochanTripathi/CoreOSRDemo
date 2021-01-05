using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string DOB { get; set; }
        public string JobTitleId { get; set; }
        public string CompanyId { get; set; }
        public double Salary { get; set; }
        public string IsAvailable { get; set; }

    }
}
