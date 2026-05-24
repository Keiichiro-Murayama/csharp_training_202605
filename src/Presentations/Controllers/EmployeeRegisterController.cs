using Microsoft.AspNetCore.Mvc;
using src.Applications.Services;
using src.Presentations.ViewModels;
using src.Exceptions;
namespace src.Presentations.Controllers;
/// <summary>
/// 従業員登録コントローラ
/// </summary>
[Route("EmployeeRegister")]
public class EmployeeRegisterController : Controller
{
    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<EmployeeRegisterController> _logger;
    /// <summary>
    /// 従業員登録サービスインターフェイス
    /// </summary>
    private readonly IEmployeeRegisterService _employeeRegisterService;
    /// <summary>
    /// 従業員登録ViewModelをEmployeeに変換するアダプター
    /// </summary>
    private readonly EmployeeRegisterViewModelAdapter _adapter;
    /// <summary>
    /// TempDataを通じて一時的にViewModelを保存・復元するためのクラス
    /// </summary>
    private readonly TempDataStore<EmployeeRegisterViewModel> _empDataStore;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="logger">ロガー</param>
    /// <param name="employeeRegisterService">従業員登録サービスインターフェイス</param>
    /// <param name="employeeRegisterViewModelAdapter">従業員登録ViewModelをEmployeeに変換するアダプター</param>
    /// <param name="empDataStore">TempDataを通じて一時的にViewModelを保存・復元するためのクラス</param>
    public EmployeeRegisterController(
        ILogger<EmployeeRegisterController> logger,
        IEmployeeRegisterService employeeRegisterService,
        EmployeeRegisterViewModelAdapter employeeRegisterViewModelAdapter,
        TempDataStore<EmployeeRegisterViewModel> empDataStore)
    {
        _logger = logger;
        _employeeRegisterService = employeeRegisterService;
        _adapter = employeeRegisterViewModelAdapter;
        _empDataStore = empDataStore;
    }

    /// <summary>
    /// 従業登録(入力)画面表示 アクションメソッド
    /// </summary>
    /// <returns></returns>
    [HttpGet("Enter")]
    public IActionResult Enter()
    {
        EmployeeRegisterViewModel? viewModel = null;
        // [戻る]ボタンへの対応
        // TempDataからEmployeeRegisterViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // 従業員登録ViewModelを生成する
            viewModel = new EmployeeRegisterViewModel();
        }
        // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
        PopulateDepartments(viewModel);
        PopulateEmpStatus(viewModel);
        // viewModelをviewに渡して画面表示する
        return View(viewModel);
    }

    /// <summary>
    /// 入力画面の[完了]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("Confirm")]
    public IActionResult Confirm(EmployeeRegisterViewModel viewModel)
    {
        // バリデーションチェック
        if (!ModelState.IsValid) // バリデーションエラーあり
        {
            // 部署一覧を取得してViewModelに設定する(SelectListItem形式)
            PopulateDepartments(viewModel);
            PopulateEmpStatus(viewModel);
            // 入力画面の表示
            return View("Enter", viewModel);
        }
        // 選択された部署のIdで部署データを取得する
        if (viewModel.DepId.HasValue)
        {
            var department = _employeeRegisterService.GetById(viewModel.DepId.Value);
            _logger.LogInformation($"部署Id:{viewModel.DepId.Value}の部署を取得する");
            // ViewModelに部署名を設定する
            viewModel.DepName = department.Name;
        }
        else
        {
            viewModel.DepName = string.Empty;
        }

        // 選択された雇用形態のIdで雇用形態データを取得する
        if (viewModel.EmpStatusId.HasValue)
        {
            var empStatus = _employeeRegisterService.GetEmpStatusById(viewModel.EmpStatusId.Value);
            _logger.LogInformation($"雇用形態Id:{viewModel.EmpStatusId.Value}の雇用形態を取得する");
            // ViewModelに雇用形態名を設定する
            viewModel.EmpStatusName = empStatus.Name;
        }
        else
        {
            viewModel.EmpStatusName = string.Empty;
        }

        // 確認画面を表示する
        return View(viewModel);
    }

    /// <summary>
    /// 確認画面の[登録]ボタンクリックアクションメソッド
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost("Regiter")]
    public IActionResult Register(EmployeeRegisterViewModel viewModel)
    {
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 登録処理GETアクションメソッドにリダイレクトする
        return RedirectToAction("Complete");
    }

    /// <summary>
    /// アクションメソッド:Regiter()のリダイレクト先
    /// PRGパターン
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    public IActionResult Complete()
    {
        EmployeeRegisterViewModel? viewModel = null;
        // TempDataからEmployeeRegisterViewModelを取得する
        viewModel = _empDataStore.Load(this);
        if (viewModel == null)
        {
            // データが存在しない場合、入力画面にリダイレクト
            return RedirectToAction("Enter");
        }

        // EmployeeRegisterFormをドメインモデル:Employeeに変換する
        var employee = _adapter.Restore(viewModel!);
        try
        {
            // 新しい従業員を登録する
            _employeeRegisterService.Register(employee);
            return View(viewModel);
        }
        catch (ExistsException ex)
        {
            ModelState.AddModelError("Email", ex.Message);
            // 部署・雇用形態一覧を取得してViewModelに設定する(SelectListItem形式)
            PopulateDepartments(viewModel);
            PopulateEmpStatus(viewModel);
            return View("Enter", viewModel);
        }
    }

    /// <summary>
    /// 確認画面の[戻る]ボタンクリックアクションメソッド
    /// </summary>
    /// <returns></returns> 
    [HttpPost("Back")]
    public IActionResult Back(EmployeeRegisterViewModel viewModel)
    {
        _logger.LogInformation("[戻る]ボタンクリック:{0}", viewModel!.ToString());
        // EmployeeRegisterViewModelをシリアライズして、TempDataに保存する
        _empDataStore.Save(this, viewModel);
        // 入力画面を出力するアクションメソッドにリダイレクトする
        return RedirectToAction("Enter");
    }

    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateDepartments(EmployeeRegisterViewModel viewModel)
    {
        // 従業員登録サービスから部署一覧を取得する
        var departments = _employeeRegisterService.GetDepartments();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetDepartments(departments);
        _logger.LogInformation("部署リストを設定");
    }

    /// <summary>
    /// 部署一覧を取得してViewModelに設定する(SelectListItem形式)
    /// </summary>
    private void PopulateEmpStatus(EmployeeRegisterViewModel viewModel)
    {
        System.Console.WriteLine("<<<<<<<<<<<<<<<<<< StartPopulateEmpStatus >>>>>>>>>>>>>>>>>>>>>>");
        // 従業員登録サービスから部署一覧を取得する
        var empstatus = _employeeRegisterService.GetEmpStatuses();
        // 部署一覧をEmployeeRegisterViewModelに登録する
        viewModel.SetEmpStatuses(empstatus);
        _logger.LogInformation("雇用形態リストを設定");
    }
}