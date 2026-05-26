using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Applications.Domains;
using src.Presentations.ViewModels;
using src.Exceptions;

namespace src.Presentations.Controllers;

[Route("DepartmentShowList")]
public class DepartmentShowListController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<DepartmentShowListController> _logger;
    /// <summary>
    /// 部署一覧サービスインターフェイス
    /// </summary>
    private readonly IDepartmentShowListService _departmentShowListService;
    /// <summary>
    /// 部署一覧ViewModelをDepartmentに変換するアダプター
    /// </summary>
    private readonly DepartmentShowListViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<DepartmentShowListViewModel> _tempDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="departmentShowListService">部署一覧サービスインターフェイス</param>
    /// <param name="departmentShowListViewModelAdapter">部署一覧ViewModelをDepartmentに変換するアダプター</param>
    /// <param name="tempDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public DepartmentShowListController(
        ILogger<DepartmentShowListController> logger,
        IDepartmentShowListService departmentShowListService,
        DepartmentShowListViewModelAdapter departmentShowListViewModelAdapter,
        TempDataStore<DepartmentShowListViewModel> tempDataStore)
    {
        _logger = logger;
        _departmentShowListService = departmentShowListService;
        _adapter = departmentShowListViewModelAdapter;
        _tempDataStore = tempDataStore;
    }

    [HttpGet("List")]
    public IActionResult List()
    {
        List<Department> departments = _departmentShowListService.GetDepartments();
        List<DepartmentShowListViewModel> departmentShowListViewModels = new List<DepartmentShowListViewModel>();
        foreach (var department in departments)
        {
            DepartmentShowListViewModel departmentShowListViewModel = _adapter.Convert(department);
            departmentShowListViewModels.Add(departmentShowListViewModel);
        }
        return View(departmentShowListViewModels);
    }
}
