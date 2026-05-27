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
public class EmpStatusRepositoryTestsNonContext
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
            .UseNpgsql()
            .Options;

        _context = new AppDbContext(options);

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init.sql");
        var sql = File.ReadAllText(path);
        // _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmpStatusRepository(_context, empStatusAdapter);
    }

    [TestMethod]
    public void FindAll_NonContext()
    {

        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindAll();
        }
        );
        Assert.AreEqual("すべての雇用形態を取得できませんでした。", ex.Message);
    }

    
    [TestMethod]
    public void FindById_NonContext()
    {
        int searchId = 1; 
        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindById(searchId);
        }
        );
        Assert.AreEqual("指定された雇用形態Idの雇用形態を取得できませんでした。", ex.Message);
    }
}