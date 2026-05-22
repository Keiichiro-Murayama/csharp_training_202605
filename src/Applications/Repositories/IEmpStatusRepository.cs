using src.Applications.Domains;
namespace src.Applications.Repositories;
/// <summary>
/// ドメインオブジェクト:部署のCRUD操作インターフェイス
/// </summary>
public interface IEmpStatusRepository
{
    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns>部署のリスト</returns>
    List<EmpStatus> FindAll();

    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    EmpStatus? FindById(int id);
}