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
services.AddSingleton<ISqlSugarClient>(sqlSugarScope); // SqlSugar �����Ƽ��õ���ģʽע��
services.AddScoped(typeof(IBaseRepository<>), typeof(SqlSugarBaseRepository<>)); // ע��ִ�
services.AddTransient<IUnitOfWork, SqlSugarUnitOfWork>(); // ע�빤����Ԫ
services.AddControllers(options =>
{
    // ��� ������Ԫ������ 
    options.Filters.Add<UnitOfWorkFilter>();
});

var app = builder.Build();

app.MapControllers();

app.Run();
