﻿//<TunynetCopyright>
//--------------------------------------------------------------
//<copyright>拓宇网络科技有限公司 2005-2012</copyright>
//<version>V0.5</verion>
//<createdate>2012-09-21</createdate>
//<author>mazq</author>
//<email>mazq@tunynet.com</email>
//<log date="2012-09-21" version="0.5">创建</log>
//--------------------------------------------------------------
//</TunynetCopyright>

using Tunynet.Common;

namespace Spacebuilder.Blog
{

    /// <summary>
    /// 文章动态项
    /// </summary>
    public static class ActivityItemKeysExtension
    {
        /// <summary>
        /// 发布文章
        /// </summary>
        public static string CreateBlogThread(this ActivityItemKeys activityItemKeys)
        {
            return "CreateBlogThread";
        }

        /// <summary>
        /// 文章评论
        /// </summary>
        public static string CreateBlogComment(this ActivityItemKeys activityItemKeys)
        {
            return "CreateBlogComment";
        }
    }

}
