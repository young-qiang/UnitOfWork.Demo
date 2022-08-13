using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Infrastructure
{
    public class SqlSugarUnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public SqlSugarUnitOfWork(ISqlSugarClient sqlSugarClient)
        {
            this._sqlSugarClient = sqlSugarClient;
        }
        public void BeginTransaction(ActionExecutingContext context)
        {
            _sqlSugarClient.AsTenant().BeginTran();
        }

        public void CommitTransaction(ActionExecutedContext resultContext)
        {
            _sqlSugarClient.AsTenant().CommitTran();
        }

        public void OnCompleted(ActionExecutingContext context, ActionExecutedContext resultContext)
        {
            _sqlSugarClient.Dispose();
        }

        public void RollbackTransaction(ActionExecutedContext resultContext)
        {
            _sqlSugarClient.AsTenant().RollbackTran();
        }
    }
}
