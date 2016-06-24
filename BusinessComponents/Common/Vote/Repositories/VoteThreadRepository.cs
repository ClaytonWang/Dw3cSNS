using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tunynet.Caching;
using Tunynet.Repositories;
using Tunynet.Utilities;

namespace Tunynet.Common.Repositories
{
    class VoteThreadRepository : Repository<VoteThread>,IVoteThreadRepository
    {
        /// <summary>
        /// 条件查找投票
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
        public PagingDataSet<VoteThread> GetVoteThreads(AuditStatus? auditStatus = null, long? voteId = null, string subjectKeyword = null, long? userId = null, DateTime? minDate = null, DateTime? maxDate = null, int pageSize = 20, int pageIndex = 1) 
        {
            var sql = Sql.Builder.Select("spb_VoteThreads.Id").From("spb_VoteThreads");
            var whereSql = Sql.Builder;
            if (auditStatus.HasValue)
            {
                whereSql.Where("spb_VoteThreads.AuditStatus=@0", auditStatus.Value);
            }           
            if (userId.HasValue)
            {
                whereSql.Where("spb_VoteThreads.userId=@0", userId);
            }
            if (!string.IsNullOrEmpty(subjectKeyword))
            {
                whereSql.Where("spb_VoteThreads.title like @0", "%" + StringUtility.StripSQLInjection(subjectKeyword) + "%");
            }
            if (minDate != null)
            {
                whereSql.Where("spb_VoteThreads.DATECREATED >= @0", minDate);
            }
            DateTime max = DateTime.Now;
            if (maxDate != null)
            {
                max = maxDate.Value.AddDays(1);
            }
            whereSql.Where("spb_VoteThreads.DATECREATED < @0", max);
            sql.Append(whereSql).OrderBy("spb_VoteThreads.Id desc");
            return GetPagingEntities(pageSize, pageIndex, sql);
        }
    }
}
