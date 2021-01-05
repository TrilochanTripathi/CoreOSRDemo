using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreOSR.DxGridDemo.Dto;

namespace CoreOSR.DxGridDemo
{
   public interface IDxDataGridAppService
   {
       Task<List<DxDataGridDto>> GetDummyData();
   }
}
