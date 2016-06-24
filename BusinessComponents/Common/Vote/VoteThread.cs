using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tunynet.Caching;

namespace Tunynet.Common
{
    [TableName("spb_VoteThreads")]
    [PrimaryKey("Id", autoIncrement = true)]
    //todo:需要设置缓存
    [CacheSetting(true)]
    [Serializable]
   public class VoteThread : IEntity
    {
        /// <summary>
        /// 新建实体时使用
        /// </summary>
        //todo:需要检查成员初始化的类型是否正确
        public static VoteThread New()
        {
            VoteThread voteThread = new VoteThread()
            {
                Title = string.Empty,
                Body = string.Empty,
                EndDate = DateTime.UtcNow,
                HiddenText = string.Empty,
                DateCreated = DateTime.UtcNow

            };
            return voteThread;
        }

        #region 需持久化属性

        /// <summary>
        /// 投票
        /// </summary>
        public long Id { get;set; }

        /// <summary>
        /// 租户类型
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 投票类型（图片，文字）true(文字)，false（图片）
        /// </summary>
        public bool VoteType { get; set; }

        /// <summary>
        /// 选项类型（选项个数）
        /// </summary>
        public int OptionType { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 投票结果（投票后可见，总是可见的）true:投票后可见
        /// </summary>
        public bool VoteResult { get; set; }

        /// <summary>
        /// 隐藏内容
        /// </summary>
        public string HiddenText { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int MemberCount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus AuditStatus { get; set; }

      

        #endregion

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion

    }
}
