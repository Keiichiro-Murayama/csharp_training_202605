using Microsoft.EntityFrameworkCore;
using src.Applications.Repositories;
using src.Applications.Services;
using src.Applications.Services.Impls;
using src.Infrastructures.Adapters;
using src.Infrastructures.Context;
using src.Infrastructures.Repositories;
using src.Presentations.Controllers;
using src.Presentations.ViewModels;
namespace src.Presentations.Extensions;
/// <summary>
/// 依存定義および依存性注入クラス
/// </summary>
public static class DependencyExtension
{
    /// <summary>
    /// アプリケーション全体の依存定義を設定する拡張メソッド
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    /// <param name="configuration">アプリケーション環境</param>
    public static void SettingDependencyInjection(
        this IServiceCollection services, IConfiguration configuration)
    {
        // EntityFramework Coreのインスタンス生成と依存定義
        SettingEntityFrameworkCore(configuration, services);
        // インフラストラクチャ層のインスタンス生成と依存定義
        SettingInfrastructures(services);
        // アプリケーション層のインスタンス生成と依存定義
        SettingApplications(services);
        // プレゼンテーション層のインスタンス生成と依存定義
        SettingPresentations(services);
    }

    /// <summary>
    /// EntityFramework Coreのインスタンス生成と依存定義
    /// </summary>
    /// <param name="configuration">アプリケーション環境</param>
    /// <param name="services">DIコンテナ</param>
    private static void SettingEntityFrameworkCore(IConfiguration configuration,  IServiceCollection services)
    {
        // 接続文字列(appsettings.json)から取得
        var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
        // DbContext登録(PostgreSQL用)
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    /// <summary>
    /// インフラストラクチャ層のインスタンス生成と依存定義
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    private static void SettingInfrastructures(IServiceCollection services)
    {
        // ドメインモデル:部署と部署エンティティの相互変換インターフェイスの実装
        services.AddScoped<DepartmentEntityAdapter>();
        // ドメインモデル:従業員と従業員エンティティの相互変換インターフェイスの実装
        services.AddScoped<EmployeeEntityAdapter>();
        // ドメインモデル:従業員ステータスとエンティティの相互変換インターフェイスの実装
        services.AddScoped<EmpStatusEntityAdapter>();
        // ドメインオブジェクト:部署のCRUD操作インターフェイス実装
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        // ドメインオブジェクト:従業員のCRUD操作インターフェイスの実装
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        // ドメインオブジェクト:従業員ステータスのCRUD操作インターフェイス実装
        services.AddScoped<IEmpStatusRepository, EmpStatusRepository>();
        
    }

    /// <summary>
    /// アプリケーション層のインスタンス生成と依存定義
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    private static void SettingApplications(IServiceCollection services)
    {
        // 従業員登録サービスインターフェイスの実装
        services.AddScoped<IEmployeeRegisterService, EmployeeRegisterService>();
        ///部署登録サービスインタフェースの実装
        services.AddScoped<IDepartmentRegisterService, DepartmentRegisterService>();
    }

    /// <summary>
    /// プレゼンテーション層のインスタンス生成と依存定義
    /// </summary>
    /// <param name="services">DIコンテナ</param>
    private static void SettingPresentations(IServiceCollection services)
    {
        // 従業員登録ViewModelをドメインオブジェクト:従業員に変換するアダプターインターフェイスの実装
        services.AddScoped<EmployeeRegisterViewModelAdapter>();
        // TempDataへのEmployeeRegisterViewの保存・復元するためのクラス
        // コンストラクタを利用して明示的にDIコンテナにインスタンスを登録する
        services.AddScoped(
            provider =>
            new TempDataStore<EmployeeRegisterViewModel>("EmployeeRegisterViewModel")
        );

        // 部署登録ViewModelをドメインオブジェクト:部署に変換するアダプターインターフェイスの実装
        services.AddScoped<DepartmentRegisterViewModelAdapter>();
        // TempDataへのDepartmentRegisterViewの保存・復元するためのクラス
        // コンストラクタを利用して明示的にDIコンテナにインスタンスを登録する
        services.AddScoped(
            provider =>
            new TempDataStore<DepartmentRegisterViewModel>("DepartmentRegisterViewModel")
        );
    }
}