using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署登録ViewModelクラス
/// </summary>
public class EmployeeRegisterViewModel
{
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "氏名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(20, ErrorMessage = "{0}は{1}文字以内で入力してください")]
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Email
    /// </summary>
    [Display(Name = "メールアドレス")]
    [EmailAddress(ErrorMessage = "メールアドレスの形式で入力してください。")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    [StringLength(50, ErrorMessage = "{0}は{1}文字以内で入力してください")]
    public string? Email { get; set; } = string.Empty;


    /// <summary>
    /// 雇用形態
    /// </summary>
    [Display(Name = "雇用形態")]
    public int? EmpStatusId { get; set; } = null;


    /// <summary>
    /// 雇用形態
    /// </summary>
    [Display(Name = "雇用形態名")]
    public string? EmpStatusName { get; set; } = string.Empty;

    /// <summary>
    /// 所属部署
    /// </summary>
    [Display(Name = "所属部署")]
    public int? DepId { get; set; } = null;

    /// <summary>
    /// 選択された部署名
    /// </summary>
    [Display(Name = "部署名")]
    public string? DepName { get; set; } = string.Empty;

    /// <summary>
    /// 部署のリストをSelectListItemのリストに変換してプロパティに設定する
    /// </summary>
    /// <param name="departments"></param>
    public void SetDepartments(List<Department> departments)
    {
        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var dept in departments)
        {
            if (dept.Id.HasValue)
            {
                var item = new SelectListItem();
                item.Value = dept.Id.Value.ToString();
                item.Text = string.IsNullOrEmpty(dept.Name) ? "(名称未設定)" : dept.Name;
                selectItems.Add(item);
            }
        }
        Departments = selectItems;
    }

    /// <summary>
    /// 雇用形態のリストをSelectListItemのリストに変換してプロパティに設定する
    /// </summary>
    /// <param name="empStatuses"></param>
    public void SetEmpStatuses(List<EmpStatus> empStatuses)
    {
        System.Console.WriteLine("<<<<<<<<<<<<<<<<<< Start SetEmpStatuses >>>>>>>>>>>>>>>>>>>>>>");

        // SelectListItemのリストを作成
        var selectItems = new List<SelectListItem>();
        foreach (var empStatus in empStatuses)
        {
            if (empStatus.Id.HasValue)
            {
                System.Console.WriteLine("<<<<<<<<<<<<<<<<<< EmpSTATUS HAS VALUE >>>>>>>>>>>>>>>>>>>>>>");

                var item = new SelectListItem();
                item.Value = empStatus.Id.Value.ToString();
                item.Text = string.IsNullOrEmpty(empStatus.Name) ? "(名称未設定)" : empStatus.Name;
                selectItems.Add(item);
            }
        }
        EmpStatuses = selectItems;
    }

    // 部署&雇用形態のリスト
    public List<SelectListItem>? Departments { get; set; } = null;
    public List<SelectListItem>? EmpStatuses { get; set; } = null;

}
