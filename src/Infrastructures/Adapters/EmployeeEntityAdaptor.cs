using src.Applications.Adapters;
using src.Applications.Domains;
using src.Infrastructures.Entities;
using src.Infrastructures.Repositories;
namespace src.Infrastructures.Adapters;
/// <summary>
/// ドメインオブジェクト:EmployeeとEmployeeEntityの相互変換インターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeEntity</typeparam>
public class EmployeeEntityAdapter :
IConverter<Employee, EmployeeEntity>, IRestorer<Employee, EmployeeEntity>
{
    private readonly DepartmentRepository _departmentRepository;
    private readonly EmpStatusRepository _empStatusRepository;

    public EmployeeEntityAdapter(DepartmentRepository departmentRepository, EmpStatusRepository empStatusRepository)
    {
        _departmentRepository = departmentRepository;
        _empStatusRepository = empStatusRepository;
    }

    /// <summary>
    /// ドメインオブジェクト:EmployeeをEmployeeEntityに変換する
    /// </summary>
    /// <param name="domain">ドメインモデル:従業員</param>
    /// <returns>EmployeeEntity</returns>
    public EmployeeEntity Convert(Employee domain)
    {
        var entity = new EmployeeEntity
        {
            EmpName = domain.Name

        };
        if (domain.Email != null)
        {
            entity.EmpEmail = domain.Email;
        }
        if (domain.Id != null)
        {
            entity.EmpId = domain.Id.Value;
        }
        if (domain.EmpStatus != null)
        {
            entity.EmpStatusId = domain.EmpStatus.Id;
        }
        if (domain.Department != null)
        {
            entity.DepId = domain.Department.Id;
        }
        return entity;
    }

    /// <summary>
    /// EmployeeEntityからドメインオブジェクト:Employeeを復元する
    /// </summary>
    /// <param name="target">EmployeeEntity</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeEntity target)
    {
        //Employeeオブジェクトに渡すDepartmentobjectに変換
        Department? foundDep ;
        if (target.DepId is not null)
        {
            foundDep = _departmentRepository.FindById(target.DepId.Value);
        } else
        {
            foundDep = null;
        }
        //Employeeオブジェクトに渡すDepartmentobjectに変換
        EmpStatus? foundEmpStatus ;
        if (target.EmpStatusId is not null)
        {
            foundEmpStatus = _empStatusRepository.FindById(target.EmpStatusId.Value);
        } else
        {
            foundEmpStatus = null;
        }
        var employee = new Employee(
            target.EmpId,
            target.EmpName,
            target.EmpEmail,
            foundEmpStatus,
            foundDep
        );
        return employee;
    }
}