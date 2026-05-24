using src.Applications.Adapters;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// EmployeeRegisterViewModel(従業員登録ViewModel)を
/// ドメインオブジェクト:Employeeに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Employee</typeparam>
/// <typeparam name="TTarget">EmployeeRegisterForm</typeparam>
public class EmployeeRegisterViewModelAdapter : IRestorer<Employee, EmployeeRegisterViewModel>
{
    /// <summary>
    /// EmployeeRegisterViewModelをドメインオブジェクト:Employeeに変換する
    /// </summary>
    /// <param name="target">EmployeeRegisterViewModel</param>
    /// <returns>ドメインオブジェクト:Employee</returns>
    public Employee Restore(EmployeeRegisterViewModel target)
    {
        // Department(部署)を作成する
        System.Console.WriteLine("<<<<<<<<<<<<<<<<<< START RESTORE >>>>>>>>>>>>>>>>>>>>>>");

        var department = new Department(target.DepId, target.DepName);
        System.Console.WriteLine("<<<<<<<<<<<<<<<<<< FIN RESTORE department  >>>>>>>>>>>>>>>>>>>>>>");
        System.Console.WriteLine(target.EmpStatusName);

        var empStatus = new EmpStatus(target.EmpStatusId, target.EmpStatusName);
        System.Console.WriteLine("<<<<<<<<<<<<<<<<<< FIN RESTORE department empStatus >>>>>>>>>>>>>>>>>>>>>>");

        // 登録するEmployee(従業員)を作成する
        var employee = new Employee(target.Name!, target.Email!, empStatus, department);
        return employee;
    }
}