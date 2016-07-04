//<TunynetCopyright>
//--------------------------------------------------------------
//<copyright>拓宇网络科技有限公司 2005-2012</copyright>
//<version>V0.5</verion>
//<createdate>2012-10-16</createdate>
//<author>jiangshl</author>
//<email>jiangshl@tunynet.com</email>
//<log date="2012-10-16" version="0.5">创建</log>
//--------------------------------------------------------------
//</TunynetCopyright>


using Spacebuilder.Common;
using Tunynet;
using Tunynet.Common;
using System.Collections.Generic;
using System.Linq;

namespace Spacebuilder.Blog
{
    /// <summary>
    /// 文章权限验证
    /// </summary>
    public static class AuthorizerExtension
    {
        /// <summary>
        /// 撰写文章/转载文章
        /// 空间主人撰写空间用户的文章
        /// </summary>
        public static bool BlogThread_Create(this Authorizer authorizer, string spaceKey)
        {
            string errorMessage = string.Empty;
            return authorizer.BlogThread_Create(spaceKey, out errorMessage);
        }

        /// <summary>
        /// 撰写文章/转载文章
        /// 空间主人撰写空间用户的文章
        /// </summary>
        public static bool BlogThread_Create(this Authorizer authorizer, string spaceKey, out string errorMessage)
        {
            IUser currentUser = UserContext.CurrentUser;
            errorMessage = "没有权限写文章";

            if (currentUser == null)
            {
                errorMessage = "您必须先登录，才能写文章";
                return false;
            }

            bool result = spaceKey == currentUser.UserName && authorizer.AuthorizationService.Check(currentUser, PermissionItemKeys.Instance().Blog_Create());
            if (!result && currentUser.IsModerated)
                errorMessage = Resources.Resource.Description_ModeratedUser_CreateBlogThreadDenied;
            return result;
        }

        /// <summary>
        /// 撰写文章/转载文章(暂时用于移动端)
        /// 空间主人撰写空间用户的文章
        /// </summary>
        public static bool BlogThread_Create(this Authorizer authorizer, IUser currentUser, out string errorMessage)
        {
            //IUser currentUser = UserContext.CurrentUser;
            errorMessage = "没有权限写文章";

            if (currentUser == null)
            {
                errorMessage = "您必须先登录，才能写文章";
                return false;
            }

            bool result = authorizer.AuthorizationService.Check(currentUser, PermissionItemKeys.Instance().Blog_Create());
            if (!result && currentUser.IsModerated)
                errorMessage = Resources.Resource.Description_ModeratedUser_CreateBlogThreadDenied;
            return result;
        }

        /// <summary>
        /// 编辑文章
        /// 空间主人或管理员可以编辑空间用户的文章（置顶也使用该规则）
        /// </summary>
        public static bool BlogThread_Edit(this Authorizer authorizer, BlogThread blogThread)
        {
            IUser currentUser = UserContext.CurrentUser;

            if (currentUser == null)
            {
                return false;
            }

            if (blogThread.UserId == currentUser.UserId || authorizer.IsAdministrator(BlogConfig.Instance().ApplicationId))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 删除文章
        /// 空间主人或管理员可以删除空间用户的文章
        /// </summary>
        public static bool BlogThread_Delete(this Authorizer authorizer, BlogThread blogThread)
        {
            IUser currentUser = UserContext.CurrentUser;
            if (currentUser == null)
            {
                return false;
            }

            if (blogThread.UserId == currentUser.UserId || authorizer.IsAdministrator(BlogConfig.Instance().ApplicationId))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 查看文章
        /// 仅自己可见的只能是文章作者或管理员可以查看
        /// 部分可见的只能是文章作者、指定可见的用户或管理员可以查看
        /// </summary>
        public static bool BlogThread_View(this Authorizer authorizer, BlogThread blogThread)
        {
            if (blogThread.PrivacyStatus == PrivacyStatus.Public)
            {
                return true;
            }

            IUser currentUser = UserContext.CurrentUser;
            if (currentUser == null)
            {
                return false;
            }

            if (blogThread.UserId == currentUser.UserId || authorizer.IsAdministrator(BlogConfig.Instance().ApplicationId))
            {
                return true;
            }

            if (blogThread.PrivacyStatus == PrivacyStatus.Private)
            {
                return false;
            }

            ContentPrivacyService contentPrivacyService = new ContentPrivacyService();
            if (contentPrivacyService.Validate(blogThread, currentUser.UserId))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 管理文章
        /// 管理员可以管理所有文章，精华、管理员推荐也使用该规则
        /// </summary>
        public static bool BlogThread_Manage(this Authorizer authorizer)
        {
            if (authorizer.IsAdministrator(BlogConfig.Instance().ApplicationId))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 关注文章
        /// 必须是登录用户
        /// </summary>
        public static bool BlogThread_Follow(this Authorizer authorizer)
        {
            if (UserContext.CurrentUser != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 评论文章
        /// 1.锁定的文章不允许评论；
        /// 2.对于匿名用户，根据站点匿名用户；
        /// </summary>
        public static bool BlogComment_Create(this Authorizer authorizer, BlogThread blogThread)
        {
            //锁定的文章不允许评论
            if (blogThread.IsLocked)
            {
                return false;
            }

            //允许登录用户
            if (UserContext.CurrentUser != null)
            {
                return true;
            }

            //判断是否允许匿名用户评论
            ISettingsManager<SiteSettings> siteSettingsManager = DIContainer.Resolve<ISettingsManager<SiteSettings>>();
            SiteSettings siteSettings = siteSettingsManager.Get();

            return siteSettings.EnableAnonymousPosting;
        }
    }
}