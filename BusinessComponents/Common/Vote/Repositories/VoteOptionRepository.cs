using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tunynet.Caching;
using Tunynet.Repositories;

namespace Tunynet.Common.Repositories
{
    class VoteOptionRepository : Repository<VoteOption>,IVoteOptionRepository
    {
        /// <summary>
        /// 获取投票选项
        /// </summary>
        /// <param name="VoteId"></param>
        /// <returns></returns>
        public IEnumerable<VoteOption> GetVoteOptions(long voteId)
        {

            Database database = CreateDAO();
            var sql = PetaPoco.Sql.Builder;
            sql.Select("*").From("spb_VoteOptions")
                .Where("VoteId=@0", voteId);
            return database.Fetch<VoteOption>(sql);
        }
    }
}
