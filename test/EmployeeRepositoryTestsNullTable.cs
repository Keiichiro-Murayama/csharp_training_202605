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
public class EmployeeRepositoryTestsNullTable
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
            .UseNpgsql(ConnectionString)
            // ConnectionStringを外す
            .Options;

        _context = new AppDbContext(options);

        var departmentAdapter = new DepartmentEntityAdapter();
        var empStatusAdapter = new EmpStatusEntityAdapter();
        _departmentRepository = new DepartmentRepository(_context, departmentAdapter);
        _empStatusRepository = new EmpStatusRepository(_context, empStatusAdapter);
        var employeeAdapter = new EmployeeEntityAdapter(_departmentRepository, _empStatusRepository);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init_null.sql");
        var sql = File.ReadAllText(path);
        // _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, employeeAdapter);
    }

        [TestMethod]
    public void FindAll_NonHit()
    {
        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindAll();
        }
        );
        Assert.AreEqual("登録された従業員記録はありません。", ex.Message);
    }

}