using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;
using Tunynet.Caching;

namespace Tunynet.Common
{
    [TableName("spb_VoteOptions")]
    [PrimaryKey("Id", autoIncrement = true)]
    //todo:需要设置缓存
    [CacheSetting(true)]
    [Serializable]
   public  class VoteOption : IEntity
    {
        /// <summary>
        /// 新建实体时使用
        /// </summary>
        //todo:需要检查成员初始化的类型是否正确
        public static VoteOption New()
        {
            VoteOption voteOption = new VoteOption()
            {
                FeaturedImage = string.Empty,
                LinkPath = string.Empty,
                OptionName = string.Empty

            };
            return voteOption;
        }

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///投票id
        /// </summary>
        public long VoteId { get; set; }

        /// <summary>
        ///图片路径
        /// </summary>
        public string FeaturedImage { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string LinkPath { get; set; }

        /// <summary>
        ///选项描述
        /// </summary>
        public string OptionName { get; set; }

        /// <summary>
        /// 票数
        /// </summary>
        public int VoteCount { get; set; }

      
        #endregion

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion
    }
}
