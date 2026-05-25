using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Applications.Domains;
namespace src.Presentations.ViewModels;
/// <summary>
/// 部署登録ViewModelクラス
/// </summary>
public class DepartmentRegisterViewModel
{
    /// <summary>
    /// 氏名
    /// </summary>
    [Display(Name = "部署名")]
    [Required(ErrorMessage = "{0}は入力必須です。")]
    public string Name { get; set; } = string.Empty;
}
