using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using UnitOfWork.Infrastructure;

namespace UnitOfWork.Demo.Filters
{
    /// <summary>
    /// 工作单元Action过滤器
    /// </summary>
    public class UnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
    {

        private readonly ILogger<UnitOfWorkFilter> _logger;
        public UnitOfWorkFilter(ILogger<UnitOfWorkFilter> logger)
        {
            this._logger = logger;
        }
        /// <summary>
        /// 过滤器排序
        /// </summary>
        internal const int FilterOrder = 9999;

        /// <summary>
        /// 排序属性
        /// </summary>
        public int Order => FilterOrder;

        /// <summary>
        /// 拦截请求
        /// </summary>
        /// <param name="context">动作方法上下文</param>
        /// <param name="next">中间件委托</param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取动作方法描述器
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var method = actionDescriptor.MethodInfo;

            // 获取请求上下文
            var httpContext = context.HttpContext;

            // 如果没有定义工作单元过滤器，则跳过
            if (!method.IsDefined(typeof(UnitOfWorkAttribute), true))
            {
                // 调用方法
                _ = await next();

                return;
            }

            // 打印工作单元开始消息
            _logger.LogInformation($@"{nameof(UnitOfWorkFilter)} Beginning");

            // 解析工作单元服务
            var _unitOfWork = httpContext.RequestServices.GetRequiredService<IUnitOfWork>();

            // 调用开启事务方法
            _unitOfWork.BeginTransaction(context);

            // 获取执行 Action 结果
            var resultContext = await next();

            if (resultContext == null || resultContext.Exception == null)
            {
                // 调用提交事务方法
                _unitOfWork.CommitTransaction(resultContext);
            }
            else
            {
                // 调用回滚事务方法
                _unitOfWork.RollbackTransaction(resultContext);
            }

            // 调用执行完毕方法
            _unitOfWork.OnCompleted(context, resultContext);

            // 打印工作单元结束消息  
            _logger.LogInformation($@"{nameof(UnitOfWorkFilter)} Ending");

        }
    }
}
