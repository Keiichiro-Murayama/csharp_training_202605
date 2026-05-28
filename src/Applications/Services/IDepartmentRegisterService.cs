using src.Applications.Domains;
namespace src.Applications.Services;
/// <summary>
/// 従業員登録サービスインターフェイス
/// </summary>
public interface IDepartmentRegisterService 
{
  
    /// <summary>
    /// 新しい従業員を登録する
    /// </summary>
    /// <param name="Department"></param>
    void Register(Department department);
}