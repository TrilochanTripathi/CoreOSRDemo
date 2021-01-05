using CoreOSR.DxGridDemo.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Newtonsoft.Json;

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
        public  GenderMasterOutputDto GetGender()
        {
            //kept here for demo only. Ideally should go to db through adding a migration
            return new GenderMasterOutputDto
            {
                data =
                    new List<KeyValueResultOutput> {
                        new KeyValueResultOutput { Text="Male",Value="ML"},
                        new KeyValueResultOutput { Text="Female",Value="FML"}
                    }
            };
        }
        [Abp.Web.Models.DontWrapResult]
        public  JobTitleMasterOutputDto GetJobTitles()
        {
            //kept here for demo only. Ideally should go to db through adding a migration
            return new JobTitleMasterOutputDto
            {
                data = new List<KeyValueResultOutput>
                {
                    new KeyValueResultOutput {Text = "Lead Developer", Value = "LDR"},
                    new KeyValueResultOutput {Text = "Developer", Value = "DVR"},
                    new KeyValueResultOutput {Text = "Software Enginner", Value = "SWR"},
                    new KeyValueResultOutput {Text = "Data Scientist", Value = "DST"},
                    new KeyValueResultOutput {Text = "Executive", Value = "EXTV"},
                    new KeyValueResultOutput {Text = "Marketing Head", Value = "MKTH"}

                }
            };
        }

        [Abp.Web.Models.DontWrapResult]
        public CompanyMasterOutputDto GetCompanies()
        {
            // kept here for demo only. Ideally should go to db through adding a migration
             return new CompanyMasterOutputDto
            {
                data = new List<KeyValueResultOutput>
                {
                    new KeyValueResultOutput {Text = "Microsoft", Value = "MSFT"},
                    new KeyValueResultOutput {Text = "Google", Value = "GGL"},
                    new KeyValueResultOutput {Text = "Amazon", Value = "AMZN"},
                    new KeyValueResultOutput {Text = "IBM", Value = "IBM"},
                    new KeyValueResultOutput {Text = "Infosys", Value = "INFY"},
                    new KeyValueResultOutput {Text = "Tata consl. services", Value = "TCS"}

                }
            };
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
