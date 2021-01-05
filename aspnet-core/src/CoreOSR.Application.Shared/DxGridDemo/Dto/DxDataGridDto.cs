using System;

namespace CoreOSR.DxGridDemo.Dto
{
   public class DxDataGridDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string CompanyId { get; set; }
        public string GenderId { get; set; }
        public string JobTitleId { get; set; }
        public double Salary { get; set; }
        public bool IsAvailable { get; set; }
    }
}
