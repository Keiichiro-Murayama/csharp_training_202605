using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace src.Infrastructures.Entities;
/// <summary>
/// 従業員テーブル(employee)を扱うEntity Framework Coreのエンティティクラス
/// </summary>
[Table("employee")]
public class EmployeeEntity
{
    /// <summary>
    /// 従業員Id(主キー)
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 従業員名
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>Eメール</summary>
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>EmpStatus</summary>
    [Column("emp_status")]
    public string EmpStatus { get; set; } = string.Empty;

    /// <summary>
    /// 所属部署Id(外部キー)
    /// </summary>
    [ForeignKey("dep_id")]
    public int DeptId { get; set; }

    /// <summary>所属部署名（外部キー）</summary>
    [Column("dep_name")]
    public string? DepName {get; set;}
}