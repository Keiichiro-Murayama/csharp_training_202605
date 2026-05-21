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
    public int EmpId { get; set; }

    /// <summary>
    /// 従業員名
    /// </summary>
    [Column("name")]
    public string EmpName { get; set; } = string.Empty;

    /// <summary>Eメール</summary>
    [Column("email")]
    public string EmpEmail { get; set; } = string.Empty;

    /// <summary>EmpStatus（外部キー）</summary>
    [ForeignKey("emp_status_id")]
    public string EmpStatusId { get; set; } = string.Empty;

    /// <summary>
    /// 所属部署Id(外部キー)
    /// </summary>
    [ForeignKey("dep_id")]
    public int DepId { get; set; }


}