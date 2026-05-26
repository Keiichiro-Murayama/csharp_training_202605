using src.Applications.Adapters;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// DepartmentShowListViewModel(従業員登録ViewModel)を
/// ドメインオブジェクト:Departmentに変換するアダプターインターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">Department</typeparam>
/// <typeparam name="TTarget">DepartmentShowListViewModel</typeparam>
public class DepartmentShowListViewModelAdapter : IConverter<Department, DepartmentShowListViewModel>
{
    /// <summary>
    /// DepartmentShowListViewModelをドメインオブジェクト:Departmentに変換する
    /// </summary>
    /// <param name="target">DepartmentShowListViewModel</param>
    /// <returns>ドメインオブジェクト:Department</returns>
    public DepartmentShowListViewModel Convert(Department target)
    {
        // 登録するDepartment(従業員)を作成する
        var department = new DepartmentShowListViewModel(target.Id, target.Name);
        return department;
    }
}