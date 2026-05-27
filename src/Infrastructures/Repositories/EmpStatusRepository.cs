using src.Infrastructures.Context;
using src.Applications.Domains;
using src.Applications.Repositories;
using src.Infrastructures.Adapters;
using src.Exceptions;
namespace src.Infrastructures.Repositories;
/// <summary>
/// ドメインオブジェクト:部署のCRUD操作インターフェイス実装
/// </summary>
public class EmpStatusRepository : IEmpStatusRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:部署と部署エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly EmpStatusEntityAdapter _adapter;

    public EmpStatusRepository(AppDbContext context, EmpStatusEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// すべての部署を取得する
    /// </summary>
    /// <returns>部署のリスト</returns>
    public List<EmpStatus> FindAll()
    {

        try
        {
            var entities = _context.EmpStatuses.ToList();

            var results = new List<EmpStatus>();

            foreach (var entity in entities)
            {
                results.Add(_adapter.Restore(entity));
            }
            if (results.Count == 0)
            {
                throw new InternalException("登録された雇用形態はありません。");
            }
            return results;
        }
        catch(InternalException){throw;}
    }

    /// <summary>
    /// 指定された部署Idの部署を取得する
    /// </summary>
    /// <param name="id">部署Id</param>
    /// <returns>取得して部署</returns>
    public EmpStatus? FindById(int id)
    {
        try
        {
            var result = _context.EmpStatuses.FirstOrDefault(d => d.EmpStatusId == id);
            if (result == null)
            {
                return null;
            }
            return _adapter.Restore(result);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "指定された雇用形態Idの雇用形態を取得できませんでした。", e);
        }
    }
}