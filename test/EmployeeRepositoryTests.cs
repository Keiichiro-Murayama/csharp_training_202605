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
        var employee = new Employee("桜井理", "Osamu@test.com", empStatus, department);
        _repository.Create(employee);

        var created = _context.Employees
            .Include(e => e.DepartmentEntity)
            .Include(e => e.EmpStatusEntity)
            .FirstOrDefault(e => e.EmpName == "桜井理");

        // IsNotNull(created);
        AreEqual("桜井理", created.EmpName);
    }


    [TestMethod]
    public void Create_DuplicateEmail()
    {
        var empStatus1 = new EmpStatus(1, "正社員");
        var department1 = new Department(1, "人事部");
        var employee1 = new Employee("桜井理", "Osamu@test.com", empStatus1, department1);
        _repository.Create(employee1);

        var empStatus2 = new EmpStatus(2, "契約社員");
        var department2 = new Department(2, "総務部");
        var employee2 = new Employee("山田修", "Osamu@test.com", empStatus2, department2);


        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.Create(employee2);
        }
        );

        Assert.AreEqual("従業員の永続化ができませんでした。", ex.Message);
    }


    [TestMethod]
    public void FindAll_Success()
    {
        List<Employee> employeeList = _repository.FindAll();

        var empStatus1 = new EmpStatus(1, "正社員");
        var department1 = new Department(1, "人事部");
        var employee1 = new Employee("テスト太郎", "taro@test.com", empStatus1, department1);

        var empStatus2 = new EmpStatus(1, "正社員");
        var department2 = new Department(1, "人事部");
        var employee2 = new Employee("テスト花子", "hanako@test.com", empStatus2, department2);

        var empStatus3 = new EmpStatus(1, "正社員");
        var department3 = new Department(1, "人事部");
        var employee3 = new Employee("テスト一郎", "ichiro@test.com", empStatus3, department3);

        List<Employee> expected = new List<Employee>{employee1, employee2, employee3};

        Assert.AreEqual(employee1.Name, employeeList[0].Name);
        Assert.AreEqual(employee2.Name, employeeList[1].Name);
        Assert.AreEqual(employee3.Name, employeeList[2].Name);
    }
}