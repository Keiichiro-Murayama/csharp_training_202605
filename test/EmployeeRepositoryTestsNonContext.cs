using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using src.Applications.Domains;
using src.Applications.Repositories;
using src.Infrastructures.Adapters;
using src.Infrastructures.Context;
using src.Infrastructures.Repositories;
using src.Exceptions;
using src.Infrastructures.Entities;
using src.Presentations.Controllers;
namespace test;


[DoNotParallelize]
[TestClass]
public class EmployeeRepositoryTestsNonContext
{
    private const string ConnectionString =
    "Host=localhost;Port=5432;Database=csharp_training_202605;Username=postgres;Password=training;";

    private EmployeeRepository _repository = null!;
    private AppDbContext _context = null!;
    private IDepartmentRepository _departmentRepository = null!;
    private IEmpStatusRepository _empStatusRepository = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql()
            // ConnectionStringを外す
            .Options;

        _context = new AppDbContext(options);

        var departmentAdapter = new DepartmentEntityAdapter();
        var empStatusAdapter = new EmpStatusEntityAdapter();
        _departmentRepository = new DepartmentRepository(_context, departmentAdapter);
        _empStatusRepository = new EmpStatusRepository(_context, empStatusAdapter);
        var employeeAdapter = new EmployeeEntityAdapter(_departmentRepository, _empStatusRepository);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        // _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, employeeAdapter);
    }

    [TestMethod]
    public void Create_NonContext()
    {
        var empStatus1 = new EmpStatus(1, "正社員");
        var department1 = new Department(1, "人事部");
        var employee1 = new Employee("桜井理", "Osamu@test.com", empStatus1, department1);

        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Create(employee1);
        }
        );
        Assert.AreEqual("従業員の永続化ができませんでした。", ex.Message);
    }

    [TestMethod]
    public void FindAll_NonContext()
    {
        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindAll();
        }
        );
        Assert.AreEqual("すべての従業員を取得できませんでした。", ex.Message);
    }

}