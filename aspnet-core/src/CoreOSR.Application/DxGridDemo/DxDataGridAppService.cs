using CoreOSR.DxGridDemo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Newtonsoft.Json;
using CoreOSR.DxGridDemo.ModelBinder;
using DevExtreme.AspNet.Data;

namespace CoreOSR.DxGridDemo
{
    public class DxDataGridAppService : CoreOSRAppServiceBase, IDxDataGridAppService
    {
        private readonly IRepository<Employee, int> _employeeRepository;
        private readonly IRepository<Gender, int> _genderRepository;
        private readonly IRepository<Company, int> _companyRepository;
        private readonly IRepository<JobTitle, int> _jobTitleRepository;
        public DxDataGridAppService(IRepository<Employee, int> employeeRepository,
             IRepository<Gender, int> genderRepository,
             IRepository<Company, int> companyRepository,
             IRepository<JobTitle, int> jobTitleRepository)
        {
            _employeeRepository = employeeRepository;
            _genderRepository = genderRepository;
            _companyRepository = companyRepository;
            _jobTitleRepository = jobTitleRepository;
        }

        [Abp.Web.Models.DontWrapResult]
        public async Task<List<DxDataGridDto>> GetDummyData()
        {
            var dtoObjList = new List<DxDataGridDto>();
            foreach (var employee in await _employeeRepository.GetAllListAsync())
            {
                dtoObjList.Add(new DxDataGridDto
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    GenderId = employee.GenderId,
                    DOB = employee.DOB,
                    JobTitleId = employee.JobTitleId,
                    CompanyId = employee.CompanyId,
                    Salary = employee.Salary,
                    IsAvailable = employee.IsAvailable
                });
            }
            return dtoObjList;
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<object> GetGender(DataSourceLoadOptions loadOptions)
        {

            var c = from g in _genderRepository.GetAll()
                    select new { Value=g.GenderCode,Text=g.Description};
            return await DataSourceLoader.LoadAsync(c, loadOptions);
           
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<object> GetJobTitles(DataSourceLoadOptions loadOptions)
        {
            var jobTitles = from g in _jobTitleRepository.GetAll()
                    select new { Value = g.JobCode, Text = g.Description };
            return await DataSourceLoader.LoadAsync(jobTitles, loadOptions);
        }

        [Abp.Web.Models.DontWrapResult]
        public async Task<object> GetCompanies(DataSourceLoadOptions loadOptions)
        {
            var companies = from g in _companyRepository.GetAll()
                            select new { Value = g.CompanyCode, Text = g.Description };
            return await DataSourceLoader.LoadAsync(companies, loadOptions);
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<DxDataGridDto> CreatePerson(string values)
        {
            var employee = JsonConvert.DeserializeObject<Employee>(values);

            await _employeeRepository.InsertAsync(employee);
            await CurrentUnitOfWork.SaveChangesAsync();
            return new DxDataGridDto();
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<string> UpdatePerson(int key, string values)
        {

            Employee employee = _employeeRepository.FirstOrDefault(x => x.Id == key);
            JsonConvert.PopulateObject(values, employee);


            await _employeeRepository.UpdateAsync(employee);
            await CurrentUnitOfWork.SaveChangesAsync();
            return "updated";
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<string> DeletePerson(int key)
        {
            await _employeeRepository.DeleteAsync(x => x.Id == key);

            await CurrentUnitOfWork.SaveChangesAsync();
            return "Deleted";
        }
    }
}
