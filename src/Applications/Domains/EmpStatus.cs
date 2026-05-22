using src.Exceptions;
namespace src.Applications.Domains;
/// <summary>
/// 所雇用形態を表すドメインオブジェクト
/// </summary>
public class EmpStatus
{
    public int? Id { get; private set; }  = 0;    // 雇用形態Id
    public string? Name { get; private set; } = string.Empty;    // 雇用形態名
    private const int MaxLength = 20; //雇用形態名の長さ
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id"雇用形態Id</param>
    /// <param name="name"雇用形態名</param>
    public EmpStatus(int? id, string? name)
    {
        //雇用形態名のルール検証
        validateEmpStatusName(name);
        Id = id;
        Name = name;
    }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name"雇用形態名</param>
    public EmpStatus(string? name) : this(null, name) { }
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="id"雇用形態Id</param>
    /// <returns></returns>
    public EmpStatus(int? id)
    {
        Id = id;
    }

    /// <summary>
    ///雇用形態名のルール検証
    /// </summary>
    /// <param name="name"></param>
    private void validateEmpStatusName(string? name)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("雇用形態名は必須です");
            if (name.Length > MaxLength)
                throw new DomainException($"雇用形態名は{MaxLength}文字以内で入力してください");
        }
    }

    /// <summary>
    ///雇用形態名の変更
    /// </summary>
    /// <param name="name"></param>
    public void ChangeName(string? name)
    {
        //雇用形態名のルール検証
        validateEmpStatusName(name);
        this.Name = name;
    }

    /// <summary>
    /// 等価性の検証
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not EmpStatus other) return false;
        return Id == other.Id;
    }
    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public override string ToString() => $"{Id?.ToString() ?? "未登録"}: {Name}";

}