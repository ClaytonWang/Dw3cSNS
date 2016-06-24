using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tunynet.Caching;
using Tunynet.Repositories;

namespace Tunynet.Common.Repositories
{
    class VoteRecordRepository : Repository<VoteRecord>, IVoteRecordRepository
    {

        /// <summary>
        /// 获取投票记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VoteRecord> GetVoteRecords(long voteId) 
        {
            Database database = CreateDAO();
            var sql = PetaPoco.Sql.Builder;
            sql.Select("*").From("spb_VoteRecords")
                .Where("VoteId=@0", voteId);
            return database.Fetch<VoteRecord>(sql);
        }


       /// <summary>
       /// 判断用户是否投过票了
       /// </summary>
       /// <param name="userId"></param>
       /// <returns></returns>
        public bool IsHasVoted(long userId,long voteId) 
        {
            Database database = CreateDAO();
            var sql = PetaPoco.Sql.Builder;
            sql.Select("*").From("spb_VoteRecords")
                .Where("UserId=@0", userId).Where("VoteId=@0", voteId);
            IEnumerable<VoteRecord> voteRecords=database.Fetch<VoteRecord>(sql);
            return voteRecords.Count()>0? true: false;   
        }

    }
}
