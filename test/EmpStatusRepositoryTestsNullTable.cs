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
public class EmpStatusRepositoryTestsNullTable
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

        var path = Path.Combine(AppContext.BaseDirectory, "sql", "init_null.sql");
        var sql = File.ReadAllText(path);
        _context.Database.ExecuteSqlRaw(sql);

        _repository = new EmpStatusRepository(_context, empStatusAdapter);
    }

    
    [TestMethod]
    public void FindAll_NonHit()
    {
        var ex = Assert.ThrowsException<InternalException>(() =>
        {
            _repository.FindAll();
        }
        );
        Assert.AreEqual("登録された雇用形態はありません。", ex.Message);
    }

    [TestMethod]
    public void FindById_NonHit()
    {
        int searchId = 10;
        EmpStatus? searched = _repository.FindById(searchId);
        Assert.AreEqual(null, searched?.Name);
    }
}