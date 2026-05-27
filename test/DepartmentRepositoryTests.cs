using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using src.Applications.Domains;
using src.Infrastructures.Adapters;
using src.Infrastructures.Context;
using src.Infrastructures.Repositories;
using src.Exceptions;
using src.Infrastructures.Entities;
namespace test;


[DoNotParallelize]
[TestClass]
public class DepartmentRepositoryTests
{
    private const string ConnectionString =
    "Host=localhost;Port=5432;Database=csharp_training_202605;Username=postgres;Password=training;";

    private DepartmentRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var departmentAdapter = new DepartmentEntityAdapter();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new DepartmentRepository(_context, departmentAdapter);
    }

    [TestMethod]
    public void Create_Success()
    {   
        var department = new Department("研究開発部");
        _repository.Create(department);


        var created = _context.Departments
            .FirstOrDefault(d => d.DepName =="研究開発部");

        AreEqual("研究開発部", created.DepName);
    }

    [TestMethod]
    public void Create_DuplicateName()
    {   
        var departmentDup = new Department("人事部");

        var ex = Assert.ThrowsException<InternalException>(()=>
        {
            _repository.Create(departmentDup);
        }
        );

        Assert.AreEqual("部署の永続化ができませんでした。", ex.Message);
    }
    
    [TestMethod]
    public void FindAll_Success()
    {
        List<Department> departmentList = _repository.FindAll();

        var department1 = new Department("人事部");
        var department2 = new Department("総務部");
        var department3 = new Department("経理部");


        List<Department> expected = new List<Department>{department1, department2, department3};

        Assert.AreEqual(department1.Name, departmentList[0].Name);
        Assert.AreEqual(department2.Name, departmentList[1].Name);
        Assert.AreEqual(department3.Name, departmentList[2].Name);
    }

    [TestMethod]
    public void FindById_Success()
    {
        int searchId = 1; 
        Department searched = _repository.FindById(searchId)!;
        Assert.AreEqual("人事部", searched.Name);
    }

    [TestMethod]
    public void FindById_ReturnNull()
    {
        int searchId = 10; 
        Department? searched = _repository.FindById(searchId);
        Assert.AreEqual(null, searched?.Name);
    }
}