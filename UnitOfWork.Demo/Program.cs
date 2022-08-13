using SqlSugar;
using UnitOfWork.Demo.Filters;
using UnitOfWork.Infrastructure;
using UnitOfWork.Repository;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

var sqlSugarScope = new SqlSugarScope(new ConnectionConfig
{
    ConnectionString = "Server=127.0.0.1;Database=TestDb;UID=sa;Password=123123",
    DbType = DbType.SqlServer,
    IsAutoCloseConnection = true, 
});
services.AddSingleton<ISqlSugarClient>(sqlSugarScope); // SqlSugar 官网推荐用单例模式注入
services.AddScoped(typeof(IBaseRepository<>), typeof(SqlSugarBaseRepository<>)); // 注入仓储
services.AddTransient<IUnitOfWork, SqlSugarUnitOfWork>(); // 注入工作单元
services.AddControllers(options =>
{
    // 添加 工作单元过滤器 
    options.Filters.Add<UnitOfWorkFilter>();
});

var app = builder.Build();

app.MapControllers();

app.Run();
