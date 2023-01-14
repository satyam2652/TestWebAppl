using System.Collections.Generic;
using System.Threading.Tasks;
using Test.WebApplication.Models;
namespace Test.WebApplication.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentMst> GetDepartmentByID(int DepartmentId);
        Task<IList<DepartmentMst>> GetAllDepartments();
        Task<OutputDto> CreateNewDepartment(DepartmentMst RequestObj);
        Task<OutputDto> UpdateDepartment(DepartmentMst RequestObj);
    }
}
