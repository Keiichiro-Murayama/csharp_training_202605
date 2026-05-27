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
namespace test;


[DoNotParallelize]
[TestClass]
public class EmployeeRepositoryTests
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
            .Options;

        _context = new AppDbContext(options);

        var departmentAdapter = new DepartmentEntityAdapter();
        var empStatusAdapter = new EmpStatusEntityAdapter();
        _departmentRepository = new DepartmentRepository(_context, departmentAdapter);
        _empStatusRepository = new EmpStatusRepository(_context, empStatusAdapter);
        var employeeAdapter = new EmployeeEntityAdapter(_departmentRepository, _empStatusRepository);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, employeeAdapter);
    }

    [TestMethod]
    public void Create_Success()
    {
        var empStatus = new EmpStatus(1, "正社員");
        var department = new Department(1, "人事部");
        var employee = new Employee("向井理", "Osamu@test.com", empStatus, department);
        _repository.Create(employee);

        var created = _context.Employees
            .Include(e => e.DepartmentEntity)
            .Include(e => e.EmpStatusEntity)
            .FirstOrDefault(e => e.EmpName == "向井理");

        // IsNotNull(created);
        AreEqual("向井理", created.EmpName);
    }

    [TestMethod]
    public void Create_DuplicateEmail()
    {
        var empStatus1 = new EmpStatus(1, "正社員");
        var department1 = new Department(1, "人事部");
        var employee1 = new Employee("向井理", "Osamu@test.com", empStatus1, department1);
        _repository.Create(employee1);

        var empStatus2 = new EmpStatus(2, "契約社員");
        var department2 = new Department(2, "総務部");
        var employee2 = new Employee("手塚治虫", "Osamu@test.com", empStatus2, department2);


        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Create(employee2);
        }
        );

        Assert.AreEqual("従業員の永続化ができませんでした。", ex.Message);
    }
}