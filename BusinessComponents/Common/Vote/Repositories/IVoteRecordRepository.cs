using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tunynet.Repositories;

namespace Tunynet.Common.Repositories
{
    public interface IVoteRecordRepository : IRepository<VoteRecord>
    {
        /// <summary>
        /// 获得投票记录
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        IEnumerable<VoteRecord> GetVoteRecords(long voteId); 
     

        /// <summary>
        /// 判断用户是否投过票了
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsHasVoted(long userId,long voteId);
    }
}
