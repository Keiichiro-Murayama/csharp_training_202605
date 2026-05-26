using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Infrastructures.Context;
using src.Applications.Repositories;
using src.Applications.Domains;
using src.Exceptions;
using src.Infrastructures.Context;

namespace src.Applications.Services.Impls;
public class EmployeeShowListService : IEmployeeShowListService
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインオブジェクト:従業員のCRUD操作インターフェイス
    /// </summary>
    private readonly IEmployeeRepository _employeeRepository;
    /// <summary>
    /// ドメインオブジェクト:部署のCRUD操作インターフェイス
    /// </summary>
    private readonly IDepartmentRepository _departmentRepository;

    private readonly IEmpStatusRepository _empStatusRepository;
        /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context">アプリケーション用DbContext</param>
    /// <param name="employeeRepository">従業員のCRUD操作インターフェイス</param>
    /// <param name="departmentRepository">部署のCRUD操作インターフェイス</param>
    public EmployeeShowListService(
        AppDbContext context,
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IEmpStatusRepository empStatusRepository)
    {
        _context = context;
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
        _empStatusRepository = empStatusRepository;
    }

    public List<Employee> GetEmployees()
    {
        return _employeeRepository.FindAll();
    }

}
