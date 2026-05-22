using src.Applications.Adapters;
using src.Applications.Domains;
using src.Infrastructures.Entities;
namespace src.Infrastructures.Adapters;
/// <summary>
/// ドメインオブジェクト:EmpStatusとEmpStatusEntityの相互変換インターフェイスの実装
/// </summary>
/// <typeparam name="TDomain">EmpStatus</typeparam>
/// <typeparam name="TTarget">EmpStatusEntity</typeparam>
public class EmpStatusEntityAdapter :
IConverter<EmpStatus, EmpStatusEntity>,IRestorer<EmpStatus, EmpStatusEntity>
{
    // <summary>
    /// ドメインオブジェクト:EmpStatusをEmpStatusEntityに変換する
    /// </summary>
    /// <param name="domain">ドメインオブジェクト:EmpStatus</param>
    /// <returns>EmpStatusEntity</returns>
    public EmpStatusEntity Convert(EmpStatus domain)
    {
        var entity = new EmpStatusEntity{
            EmpStatusName = domain.Name!,
        };
        if (domain.Id != null)
        {
            // int?（nullable int）から int への暗黙的な変換ができない
            // 明示的にValueを使う
            entity.EmpStatusId = domain.Id.Value;
        }
        return entity;
    }

    /// <summary>
    /// EmpStatusEntityからドメインオブジェクト:EmpStatusを復元する
    /// </summary>
    /// <param name="entity">EmpStatusEntity</param>
    /// <returns>ドメインオブジェクト:EmpStatus</returns>
    public EmpStatus Restore(EmpStatusEntity target)
    {
        var EmpStatus = new EmpStatus(target.EmpStatusId,target.EmpStatusName!);
        return EmpStatus;
    }
}