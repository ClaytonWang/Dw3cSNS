using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tunynet.Common.Repositories;
using Tunynet.Events;

namespace Tunynet.Common
{
  public class VoteService
    {
        IVoteOptionRepository voteOptionRepository = new VoteOptionRepository();
        IVoteRecordRepository voteRecordRepository = new VoteRecordRepository();
        IVoteThreadRepository voteThreadRepository = new VoteThreadRepository();

      /// <summary>
      /// 创建投票
      /// </summary>
      /// <param name="voteThread">投票实体</param>
      /// <returns></returns>
        public long CreateVote(VoteThread voteThread) 
        {
            if (voteThread.Body == null) { voteThread.Body = ""; }
            if (voteThread.HiddenText == null) { voteThread.HiddenText = ""; }
            long threadId = 0;
            long.TryParse(voteThreadRepository.Insert(voteThread).ToString(), out threadId);
            return threadId;
        }
        
        /// <summary>
        /// 创建投票选项
        /// </summary>
        /// <param name="voteOptions">投票选项</param>
        public void CreateVoteOption(VoteOption voteOption) 
        {
            voteOptionRepository.Insert(voteOption);
        }

        /// <summary>
        /// 修改投票
        /// </summary>
        /// <returns></returns>
        public void EditVote(VoteThread voteThread) 
        {
            voteThreadRepository.Update(voteThread);      
        }

        /// <summary>
        /// 投票
        /// </summary>
        /// <param name="voteId">投票Id</param>
        /// <param name="voteOptionId">投票选项Id</param>
        /// <param name="userId">投票人</param>
        /// <param name="isAnoymity">是否匿名</param>
        /// <returns>投票是否成功</returns>
        public bool VoteVoteOption(long voteId,long voteOptionId,long userId,bool isAnoymity) 
        {
          //投票选项
          VoteOption voteOption= voteOptionRepository.Get(voteOptionId);
          voteOption.VoteCount = voteOption.VoteCount + 1;
          voteOptionRepository.Update(voteOption);
         
          //投票记录
          VoteRecord voteRecord = new VoteRecord() { 
          VoteId=voteId,
          OptionId = voteOptionId,
          UserId = userId,
          DateCreated=DateTime.Now,
          IsAnoymity = isAnoymity,
          IP=""     
          };     
          return Convert.ToInt32( voteRecordRepository.Insert(voteRecord))>0 ? true:false ;
        }

       /// <summary>
       /// 投票
       /// </summary>
       /// <param name="voteId"></param>
       public void VoteVoteThread(long voteId)
        { 
            //投票人数
            VoteThread voteThread = voteThreadRepository.Get(voteId);
            voteThread.MemberCount = voteThread.MemberCount + 1;
            voteThreadRepository.Update(voteThread);
        
        }


   
      /// <summary>
      /// 获取投票
      /// </summary>
      /// <returns></returns>
        public VoteThread GetVoteThread(long voteId) 
        {
            return voteThreadRepository.Get(voteId);
        }

        /// <summary>
        /// 获取投票下选项
        /// </summary>
        /// <param name="voteId"></param>
        /// <returns></returns>
        public IEnumerable<VoteOption> GetVoteOptions(long voteId) 
        {
            return voteOptionRepository.GetVoteOptions(voteId);
        }

        /// <summary>
        /// 获取投票记录
        /// </summary>
        /// <param name="VoteId">投票Id</param>
        /// <returns></returns>
        public IEnumerable<VoteRecord> GetVoteRecords(long VoteId) 
        {
            return voteRecordRepository.GetVoteRecords(VoteId);
        }
        
        /// <summary>
        /// 判断是否投过票了
        /// </summary>
        /// <returns></returns>
        public bool IsHasVoted(long userId,long voteId) 
        {

            return voteRecordRepository.IsHasVoted(userId, voteId);
        }

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
            return voteThreadRepository.GetVoteThreads(auditStatus, voteId, subjectKeyword, userId, minDate, maxDate, pageSize, pageIndex);
        }

        /// <summary>
        /// 设置审核设置
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <param name="isApproved">审核状态</param>
        public void UpdateAuditStatus(long voteId, bool isApproved)
        {
            AuditStatus auditStatus = isApproved ? AuditStatus.Success : AuditStatus.Fail;
            VoteThread voteThread = voteThreadRepository.Get(voteId);
            string operationType = isApproved ? EventOperationType.Instance().Approved() : EventOperationType.Instance().Disapproved();
            if (voteThread != null && voteThread.AuditStatus != auditStatus)
            {
                AuditStatus oldAuditStatus = voteThread.AuditStatus;
                voteThread.AuditStatus = auditStatus;
                voteThreadRepository.Update(voteThread);
                //触发事件
                EventBus<VoteThread>.Instance().OnAfter(voteThread, new CommonEventArgs(operationType));
                EventBus<VoteThread, AuditEventArgs>.Instance().OnAfter(voteThread, new AuditEventArgs(oldAuditStatus, auditStatus));
            }
        }

        /// <summary>
        /// 删除投票
        /// </summary>
        /// <param name="VoteId"></param>
        public void DeleteVoteById(long voteId) 
        {
            VoteThread voteThread = voteThreadRepository.Get(voteId);
            if (voteThread!=null)
            {
                IEnumerable<VoteRecord> voteRecord = voteRecordRepository.GetVoteRecords(voteId);
                IEnumerable<VoteOption> voteOption = voteOptionRepository.GetVoteOptions(voteId);

                foreach (var item in voteRecord)
                {
                    voteRecordRepository.Delete(item);
                }
                foreach (var item in voteOption)
                {
                    voteOptionRepository.Delete(item);
                }   
                voteThreadRepository.Delete(voteThread);           
            }       
        }
    }
}
