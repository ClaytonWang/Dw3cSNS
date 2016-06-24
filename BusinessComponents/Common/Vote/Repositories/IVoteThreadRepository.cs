using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tunynet.Repositories;

namespace Tunynet.Common.Repositories
{
   public interface IVoteThreadRepository : IRepository<VoteThread>
    {
       
       /// <summary>
        /// 条件获取投票
       /// </summary>
       /// <param name="auditStatus"></param>
       /// <param name="voteId"></param>
       /// <param name="subjectKeyword"></param>
       /// <param name="userId"></param>
       /// <param name="minDate"></param>
       /// <param name="maxDate"></param>
       /// <param name="pageSize"></param>
       /// <param name="pageIndex"></param>
       /// <returns></returns>
       PagingDataSet<VoteThread> GetVoteThreads(AuditStatus? auditStatus = null, long? voteId = null, string subjectKeyword = null, long? userId = null, DateTime? minDate = null, DateTime? maxDate = null, int pageSize = 20, int pageIndex = 1);
    }
}
