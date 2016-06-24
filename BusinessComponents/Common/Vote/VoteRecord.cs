using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tunynet.Caching;

namespace Tunynet.Common
{
    [TableName("spb_VoteRecords")]
    [PrimaryKey("Id", autoIncrement = true)]
    //todo:需要设置缓存
    [CacheSetting(true)]
    [Serializable]
   public class VoteRecord : IEntity
    {

        /// <summary>
        /// 新建实体时使用
        /// </summary>
        //todo:需要检查成员初始化的类型是否正确
        public static VoteRecord New()
        {
            VoteRecord voteRecord = new VoteRecord()
            {
                DateCreated = DateTime.UtcNow,
                IP = string.Empty

            };
            return voteRecord;
        }

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get;  set; }

        /// <summary>
        ///投票id
        /// </summary>
        public long VoteId { get; set; }

        /// <summary>
        ///选项id
        /// </summary>
        public long OptionId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///添加时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsAnoymity { get; set; }

      
        #endregion

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion
    }
}
