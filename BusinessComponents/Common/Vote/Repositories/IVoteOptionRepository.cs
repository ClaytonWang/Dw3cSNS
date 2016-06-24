using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tunynet.Repositories;

namespace Tunynet.Common.Repositories
{
    public  interface IVoteOptionRepository : IRepository<VoteOption>
    {
        /// <summary>
        /// 获取选项
        /// </summary>
        /// <param name="VoteId"></param>
        /// <returns></returns>
        IEnumerable<VoteOption> GetVoteOptions(long VoteId);
    }
}
