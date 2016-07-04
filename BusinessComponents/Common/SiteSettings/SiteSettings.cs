//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tunynet.Utilities;
using System.Web;
using Tunynet.Caching;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Tunynet.Common
{
    /// <summary>
    /// 站点设置
    /// </summary>
    /// <remarks>安装站点时，必须设置MainSiteRootUrl</remarks>
    [CacheSetting(true)]
    [Serializable]
    public class SiteSettings : IEntity
    {
        string beiAnScript = string.Empty;
        /// <summary>
        /// 备案信息
        /// </summary>
        [AllowHtml]
        public string BeiAnScript
        {
            get { return beiAnScript; }
            set { beiAnScript = value; }
        }

        string statScript = string.Empty;
        /// <summary>
        /// 页脚统计脚本
        /// </summary>
        [AllowHtml]
        public string StatScript
        {
            get { return statScript; }
            set { statScript = value; }
        }

        string links = string.Empty;
        /// <summary>
        /// 页脚链接
        /// </summary>
        [AllowHtml]
        public string Links
        {
            get { return links; }
            set { links = value; }
        }


        private Guid siteKey = Guid.NewGuid();
        /// <summary>
        /// 站点唯一标识
        /// </summary>
        public Guid SiteKey
        {
            get { return siteKey; }
            set { siteKey = value; }
        }

        string defaultSiteName = "前端○简文";
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName
        {
            get { return defaultSiteName; }
            set { defaultSiteName = value; }
        }


        string defaultSiteDescription = "交流技术，沟通想法";
        /// <summary>
        /// 站点描述
        /// </summary>
        public string SiteDescription
        {
            get { return defaultSiteDescription; }
            set { defaultSiteDescription = value; }
        }

        string searchMetaDescription = "前端○简文是一个基于内容分享的社区";
        /// <summary>
        /// 页面头信息的description
        /// </summary>
        public string SearchMetaDescription
        {
            get { return searchMetaDescription; }
            set { searchMetaDescription = value; }
        }

        string searchMetaKeyWords = "前端○简文,前端开发";
        /// <summary>
        /// 页面头信息的KeyWord
        /// </summary>
        public string SearchMetaKeyWords
        {
            get { return searchMetaKeyWords; }
            set { searchMetaKeyWords = value; }
        }

        private bool enableMultilingual = false;
        /// <summary>
        /// 是否启用国际化
        /// </summary>
        public bool EnableMultilingual
        {
            get { return enableMultilingual; }
            set { enableMultilingual = value; }
        }

        private string defaultLanguage = "zh-cn";
        /// <summary>
        /// 系统默认语言
        /// </summary>
        public string DefaultLanguage
        {
            get { return defaultLanguage; }
            set { defaultLanguage = value; }
        }

        //主站点Url
        private string mainSiteRootUrl = @"http://localhost";
        /// <summary>
        /// 主站URL
        /// </summary>
        /// <remarks>
        /// 安装程序（或者首次启动时）需要自动保存该地址
        /// </remarks>
        public string MainSiteRootUrl
        {
            get { return mainSiteRootUrl; }
            set { mainSiteRootUrl = value; }
        }

        string siteTheme = "Default";
        /// <summary>
        /// 站点主题
        /// </summary>
        public string SiteTheme
        {
            get { return siteTheme; }
            set { siteTheme = value; }
        }

        private string siteThemeAppearance = string.Empty;
        /// <summary>
        /// 站点皮肤外观
        /// </summary>
        public string SiteThemeAppearance
        {
            get { return siteThemeAppearance; }
            set { siteThemeAppearance = value; }
        }

        private bool defaultEnableAnonymousPosting = false;

        /// <summary>
        /// 是否允许匿名发帖
        /// </summary>
        /// <remarks>
        /// 包括所有的：评论、留言、回帖等
        /// </remarks>
        public bool EnableAnonymousPosting
        {
            get { return defaultEnableAnonymousPosting; }
            set { defaultEnableAnonymousPosting = value; }
        }

        private bool enableAnonymousBrowse = true;
        /// <summary>
        /// 是否允许匿名用户访问站点
        /// </summary>
        public bool EnableAnonymousBrowse
        {
            get { return enableAnonymousBrowse; }
            set { enableAnonymousBrowse = value; }
        }


        private bool enableSimpleHome = false;
        /// <summary>
        /// 匿名用户默认访问
        /// </summary>
        public bool EnableSimpleHome
        {
            get { return enableSimpleHome; }
            set { enableSimpleHome = value; }
        }


        private bool shareToThirdIsEnabled = true;
        /// <summary>
        /// 分享到站外是否启用
        /// </summary>
        public bool ShareToThirdIsEnabled
        {
            get { return shareToThirdIsEnabled; }
            set { shareToThirdIsEnabled = value; }
        }

        private ShareDisplayType shareToThirddisplayType = ShareDisplayType.Icon;
        /// <summary>
        /// 分享到站外展示类型
        /// </summary>
        public ShareDisplayType ShareToThirdDisplayType
        {
            get { return shareToThirddisplayType; }
            set { shareToThirddisplayType = value; }
        }

        private ShareDisplayIconSize shareDisplayIconSize = ShareDisplayIconSize.small;
        /// <summary>
        /// 分享到站外图标形式展示大小
        /// </summary>
        public ShareDisplayIconSize ShareDisplayIconSize
        {
            get { return shareDisplayIconSize; }
            set { shareDisplayIconSize = value; }
        }

        private string shareToThirdBusiness = "Baidu";
        /// <summary>
        /// 分享到站外展示商家
        /// </summary>
        public string ShareToThirdBusiness
        {
            get { return shareToThirdBusiness; }
            set { shareToThirdBusiness = value; }
        }



        #region IEntity 成员

        object IEntity.EntityId { get { return typeof(SiteSettings).FullName; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion

    }


    /// <summary>
    /// 分享到站外展示形式
    /// </summary>
    public enum ShareDisplayType
    {
        /// <summary>
        /// 文字
        /// </summary>
        [Display(Name = "文字")]
        Word,
        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        Icon,
        /// <summary>
        /// 侧栏
        /// </summary>
        [Display(Name = "侧栏")]
        Sidebar
    }

    /// <summary>
    /// 分享到站外图标形式展示大小
    /// </summary>
    public enum ShareDisplayIconSize
    {
        /// <summary>
        /// 大图标
        /// </summary>
        [Display(Name = "大图标")]
        Big,
        /// <summary>
        /// 中图标
        /// </summary>
        [Display(Name = "中图标")]
        middle,
        /// <summary>
        /// 小图标
        /// </summary>
        [Display(Name = "小图标")]
        small
    }
}