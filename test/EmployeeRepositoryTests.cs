using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using src.Applications.Domains;
using src.Infrastructures.Adapters;
using src.Infrastructures.Context;
using src.Infrastructures.Repositories;
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

    [TestInitialize]
    public void Setup()
    {
        var employeeAdapter = new EmployeeEntityAdapter();
        // var Adapter = new ItemStockEntityAdapter();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmployeeRepository(_context, employeeAdapter);
    }

    [TestMethod]
    public void Create_Success()
    {   
        var empStatus = new EmpStatus(1, "正社員");
        var department = new Department(1,"人事部");
        var employee = new Employee("向井理","test@dogs.com", empStatus,  department);
        _repository.Create(employee);

        var created = _context.Employees
            .Include(e => e.DepId)
            .Include(e => e.EmpStatusId)
            .FirstOrDefault(e => e.EmpName =="向井理");

        IsNotNull(created);
        AreEqual("向井理", created.EmpName);



    }
}