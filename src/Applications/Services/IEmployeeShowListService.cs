using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Applications.Domains;

namespace src.Applications.Services
{
    public interface IEmployeeShowListService
    {
        ///すべての従業員を取得する
        List<Employee> GetEmployees();
    }
}