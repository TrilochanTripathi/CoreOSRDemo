using CoreOSR.DxGridDemo.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace CoreOSR.DxGridDemo
{
    public class DxDataGridAppService : CoreOSRAppServiceBase, IDxDataGridAppService
    {
        private readonly IRepository<Employee, int> _employeeRepository;
        public DxDataGridAppService(IRepository<Employee, int> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Abp.Web.Models.DontWrapResult]
        public Task<List<DxDataGridDto>> GetDummyData()
        {
            throw new NotImplementedException();
        }
    }
}
