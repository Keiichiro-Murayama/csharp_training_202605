using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace src.Infrastructures.Entities;
/// <summary>
/// EmpStatusを扱うEntity Framework Coreのエンティティクラス
/// </summary>
[Table("empStatus")]
public class EmpStatus
{
    /// <summary>
    /// 雇用形態Id(主キー)
    /// </summary> 
    [Key]
    [Column("id")]
    public int EmpStatusId { get; set; }
    /// <summary>
    /// 雇用形態名
    /// </summary> 
    [Column("name")]
    public string EmpStatusName { get; set; } = string.Empty;
}