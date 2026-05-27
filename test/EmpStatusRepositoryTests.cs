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
public class EmpStatusRepositoryTests
{
    private const string ConnectionString =
    "Host=localhost;Port=5432;Database=csharp_training_202605;Username=postgres;Password=training;";

    private EmpStatusRepository _repository = null!;
    private AppDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var empStatusAdapter = new EmpStatusEntityAdapter();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmpStatusRepository(_context, empStatusAdapter);
    }

    
    [TestMethod]
    public void FindAll_Success()
    {
        List<EmpStatus> empStatusList = _repository.FindAll();

        var empStatus1 = new EmpStatus("正社員");
        var empStatus2 = new EmpStatus("契約社員");
        var empStatus3 = new EmpStatus("アルバイト");


        List<EmpStatus> expected = new List<EmpStatus>{empStatus1, empStatus2, empStatus3};

        Assert.AreEqual(empStatus1.Name, empStatusList[0].Name);
        Assert.AreEqual(empStatus2.Name, empStatusList[1].Name);
        Assert.AreEqual(empStatus3.Name, empStatusList[2].Name);
    }

    [TestMethod]
    public void FindById_Success()
    {
        int searchId = 1; 
        EmpStatus searched = _repository.FindById(searchId)!;
        Assert.AreEqual("正社員", searched.Name);
    }

    [TestMethod]
    public void FindById_ReturnNull()
    {
        int searchId = 10; 
        EmpStatus? searched = _repository.FindById(searchId);
        Assert.AreEqual(null, searched?.Name);
    }
}