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
            //all below can be grouped into common look up repository
            _genderRepository = genderRepository;
            _companyRepository = companyRepository;
            _jobTitleRepository = jobTitleRepository;
        }

        [Abp.Web.Models.DontWrapResult]
        public async Task<List<DxDataGridDto>> GetDummyData()
        {
            // in real production code to use server side grid options DataSourceLoadOptions should be used
            //left to describe that alternate implementation is there.

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
                    select new { Value = g.GenderCode, Text = g.Description };
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


            //server side validation goes here, model state can be used or manual check possible as below
            //if(employee.Name=='') return error

            if (employee != null)
            {
                await _employeeRepository.InsertAsync(employee);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            

            //person entity can be returned after mapping it using Automapper
            //returning new object for demo only
            return new DxDataGridDto();
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<string> UpdatePerson(int key, string values)
        {

            if (key == 0)
                return "Update key invalid";

            //server side validation goes here, check if key is not null and values are as per business rules

            Employee employee = _employeeRepository.FirstOrDefault(x => x.Id == key);


            if (employee == null)
                return "Update Failed";

            JsonConvert.PopulateObject(values, employee);


            await _employeeRepository.UpdateAsync(employee);
            await CurrentUnitOfWork.SaveChangesAsync();
            return "updated";
        }
        [Abp.Web.Models.DontWrapResult]
        public async Task<string> DeletePerson(int key)
        {
            if (key == 0)
                return "Delete failed";

            await _employeeRepository.DeleteAsync(x => x.Id == key);

            await CurrentUnitOfWork.SaveChangesAsync();
            return "Deleted";
        }
    }
}
