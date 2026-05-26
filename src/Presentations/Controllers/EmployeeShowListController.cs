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

[Route("EmployeeShowList")]
public class EmployeeShowListController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeShowListController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeShowListService _employeeShowListService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeShowListViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<EmployeeShowListViewModel> _tempDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="employeeShowListService">従業員登録サービスインターフェイス</param>
    /// <param name="employeeShowListViewModelAdapter">従業員登録ViewModelをEmployeeに変換するアダプター</param>
    /// <param name="tempDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public EmployeeShowListController(
        ILogger<EmployeeShowListController> logger,
        IEmployeeShowListService employeeShowListService,
        EmployeeShowListViewModelAdapter employeeShowListViewModelAdapter,
        TempDataStore<EmployeeShowListViewModel> tempDataStore)
    {
        _logger = logger;
        _employeeShowListService = employeeShowListService;
        _adapter = employeeShowListViewModelAdapter;
        _tempDataStore = tempDataStore;
    }

    [HttpGet("List")]
    public IActionResult List()
    {
        List<Employee> employees = _employeeShowListService.GetEmployees();
        List<EmployeeShowListViewModel> employeeShowListViewModels = new List<EmployeeShowListViewModel>();
        foreach (var employee in employees)
        {
            EmployeeShowListViewModel employeeShowListViewModel = _adapter.Convert(employee);
            employeeShowListViewModels.Add(employeeShowListViewModel);
        }
        return View(employeeShowListViewModels);
    }
}
