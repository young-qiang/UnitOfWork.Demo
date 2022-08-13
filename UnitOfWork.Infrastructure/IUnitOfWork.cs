
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Infrastructure
{
    /// <summary>
    /// 工作单元依赖接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 开启工作单元处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitOfWork"></param>
        void BeginTransaction(ActionExecutingContext context);

        /// <summary>
        /// 提交工作单元处理
        /// </summary>
        /// <param name="resultContext"></param>
        /// <param name="unitOfWork"></param>
        void CommitTransaction(ActionExecutedContext resultContext);

        /// <summary>
        /// 回滚工作单元处理
        /// </summary>
        /// <param name="resultContext"></param>
        /// <param name="unitOfWork"></param>
        void RollbackTransaction(ActionExecutedContext resultContext);

        /// <summary>
        /// 执行完毕（无论成功失败）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resultContext"></param>
        void OnCompleted(ActionExecutingContext context, ActionExecutedContext resultContext);
    }
}
