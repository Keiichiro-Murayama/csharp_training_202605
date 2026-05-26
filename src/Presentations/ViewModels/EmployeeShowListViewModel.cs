using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Presentations.ViewModels;
public class EmployeeShowListViewModel
{
    [Display(Name = "従業員ID")]
    public int? Id {get; set;} 
    [Display(Name = "氏名")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "メールアドレス")]
    public string Email { get; set; } = string.Empty;
    [Display(Name = "雇用形態名")]
    public string? EmpStatusName { get; set; } = string.Empty;
    [Display(Name = "所属部署")]
    public string? DepName { get; set; } = string.Empty;

    ///コンストラクタ
    public EmployeeShowListViewModel(int? id, string name, string email,string? empStatusName, string? depName)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.EmpStatusName = empStatusName;
        this.DepName = depName;
    }
}
