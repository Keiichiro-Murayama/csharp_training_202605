using src.Applications.Adapters;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// EmployeeShowListViewModel(従業員登録ViewModel)を
/// ドメインオブジェクト:Employeeに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeShowListViewModel</typeparam>
public class EmployeeShowListViewModelAdapter : IConverter<Employee, EmployeeShowListViewModel>
{
    /// <summary>
    /// EmployeeShowListViewModelをドメインオブジェクト:Employeeに変換する
    /// </summary>
    /// <param name="target">EmployeeShowListViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public EmployeeShowListViewModel Convert(Employee target)
    {
        // 登録するEmployee(従業員)を作成する
        var employee = new EmployeeShowListViewModel(target.Id, target.Name, target.Email, target.EmpStatus?.Name, target.Department?.Name);
        return employee;
    }
}