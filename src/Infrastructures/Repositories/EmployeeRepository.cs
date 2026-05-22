using src.Infrastructures.Context;
using src.Applications.Domains;
using src.Applications.Repositories;
using src.Infrastructures.Adapters;
using src.Exceptions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
namespace src.Infrastructures.Repositories;
/// <summary>
/// ドメインオブジェクト:従業員のCRUD操作インターフェイスの実装
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    /// <summary>
    /// アプリケーション用DbContext
    /// </summary>
    private readonly AppDbContext _context;
    /// <summary>
    /// ドメインモデル:従業員と従業員エンティティの相互変換インターフェイスの実装
    /// </summary>
    private readonly EmployeeEntityAdapter _adapter;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context"></param>
    /// <param name="adapter"></param>
    public EmployeeRepository(AppDbContext context, EmployeeEntityAdapter adapter)
    {
        _context = context;
        _adapter = adapter;
    }

    /// <summary>
    /// 従業員を永続化する
    /// </summary>
    /// <param name="employee">永続化対象の従業員</param>
    public void Create(Employee employee)
    {
        try
        {
            System.Console.WriteLine("<<<<<<<<<<<<<<<<<< START ENTITY CREATE ON REPOSITORY >>>>>>>>>>>>>>>>>>>>>>");

            var entity = _adapter.Convert(employee);
            System.Console.WriteLine("<<<<<<<<<<<<<<<<<< FIN ENTITY CREATE ON REPOSITORY >>>>>>>>>>>>>>>>>>>>>>");
            System.Console.WriteLine($"entity:{entity.EmpId},{entity.EmpName},{entity.EmpEmail}, {entity.DepId},{entity.EmpStatusId}");
            _context.Employees.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new InternalException(
                "従業員の永続化ができませんでした。", e);
        }
    }

    /// <summary>
    /// 従業員リストを取得する
    /// </summary>
    /// <returns></returns>
    public List<Employee> FindAll()
    {
        return null;
    }
}