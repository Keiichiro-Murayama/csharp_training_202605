using Microsoft.EntityFrameworkCore;
using src.Infrastructures.Entities;

namespace src.Infrastructures.Context;

public class AppDbContext : DbContext
{
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }

    public DbSet<EmpStatusEntity> EmpStatuses{get; set;}

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options){}

    /// <summary>
    /// エンティティの結合
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // // EmployeeEntityのモデル構成を定義    
        // modelBuilder.Entity<EmployeeEntity>()
        //     .ToTable("department")    // itemテーブルにマッピング    
        //     .HasKey(i => i.Id); // 主キー項目の定義
        // // テーブルの列とプロパティのマッピング
        // modelBuilder.Entity<EmployeeEntity>()
        //     .Property(i => i.Id).HasColumnName("id");
        // modelBuilder.Entity<EmployeeEntity>()
        //     .Property(i => i.Name).HasColumnName("name");
        // modelBuilder.Entity<EmployeeEntity>()
        //     .Property(i => i.Email).HasColumnName("email");
        // modelBuilder.Entity<EmployeeEntity>()
        //     .Property(i => i.EmpStatus).HasColumnName("empstatus");
        //EmployeeとDepartmentの一対多のリレーション
        // modelBuilder.Entity<EmployeeEntity>()
        //     .HasOne(p = p.DepId)
        //     .WithMany(c => c.Employees)
        //     .HasForeignKey(p=> p.)

        // ItemとItemStock:1対1リレーション
        // modelBuilder.Entity<ItemEntity>()
            // .HasOne(p => p.Stock)
            // .WithOne(ps => ps.Product)
            // .HasForeignKey<ItemStockEntity>(ps => ps.ItemId)
            // // 親エンティティが削除されたときに、関連する子エンティティも自動的に削除される
            // .OnDelete(DeleteBehavior.Cascade);
    }

    
}