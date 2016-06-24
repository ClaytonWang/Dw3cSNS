using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Tunynet.Common.Repositories;
using System.Web.Mvc;

namespace Tunynet.Common
{
 public  class VoteEditModel
    {
        /// <summary>
        /// 投票id
        /// </summary>
        public long VoteId { get;  set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "请输入投票标题")]       
        [StringLength(60, ErrorMessage = "最多允许输入60个字")]       
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(255, ErrorMessage = "最多允许输入255个字")]
        public string Body { get; set; }

        /// <summary>
        /// 投票类型（文字，图片）
        /// </summary>
        public bool VoteType { get; set; }

        /// <summary>
        /// 选项类型(选项个数)
        /// </summary>
        public int OptionType { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        //[WaterMark(Content = "在此输入活动详细地址")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 投票结果（结束后可见，始终可见）
        /// </summary>
        public bool VoteResult { get; set; }

        /// <summary>
        /// 隐藏内容
        /// </summary>
        [StringLength(255, ErrorMessage = "最多允许输入255个字")]
        public string HiddenText { get; set; }

     
        /// <summary>
        /// 转换为投票实体
        /// </summary>
        /// <returns></returns>
        public VoteThread AsVoteThread()
        {
            VoteThread voteThread = new VoteThread();
            if (this.VoteId == 0)
            {              
            }
            else {
                voteThread = new VoteThreadRepository().Get(this.VoteId);           
            }

            voteThread.Title = this.Title;
            voteThread.Body = this.Body;
            voteThread.VoteType = this.VoteType;
            voteThread.OptionType = this.OptionType;
            voteThread.EndDate = this.EndDate;
            voteThread.VoteResult = this.VoteResult;
            voteThread.HiddenText = this.HiddenText;

            return voteThread;
        }

    }

    /// <summary>
    /// 群组实体的扩展类
    /// </summary>
    public static class VoteEntityExtensions
    {
        /// <summary>
        /// 将数据库中的信息转换成EditModel实体
        /// </summary>
        /// <param name="voteThread"></param>
        /// <returns></returns>
        public static VoteEditModel AsEditModel(this VoteThread voteThread)
        {
            return new VoteEditModel
            {
                VoteId = voteThread.Id,
                Title = voteThread.Title,
                Body = voteThread.Body,
                VoteType = voteThread.VoteType,
                OptionType = voteThread.OptionType,
                EndDate = voteThread.EndDate,
                VoteResult = voteThread.VoteResult,
                HiddenText = voteThread.HiddenText             
            };
        }
    }



}
