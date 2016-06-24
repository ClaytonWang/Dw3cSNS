//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using CodeKicker.BBCode;
using Spacebuilder.Common;
using Tunynet.Common;
using Tunynet.Utilities;
using System;
using System.Text.RegularExpressions;
using Tunynet;

namespace Spacebuilder.CMS
{
    /// <summary>
    /// 资讯正文解析器
    /// </summary>
    public class CmsBodyProcessor : IBodyProcessor
    {

        public string Process(string body, string tenantTypeId, long associateId, long userId)
        {
            //if (string.IsNullOrEmpty(body) || !body.Contains("[attach:"))
            //    return body;

            //List<long> attachmentIds = new List<long>();

            //Regex rg = new Regex(@"\[attach:(?<id>[\d]+)\]", RegexOptions.Multiline | RegexOptions.Singleline);
            //MatchCollection matches = rg.Matches(body);

            //if (matches != null)
            //{
            //    foreach (Match m in matches)
            //    {
            //        if (m.Groups["id"] == null || string.IsNullOrEmpty(m.Groups["id"].Value))
            //            continue;
            //        long attachmentId = 0;
            //        long.TryParse(m.Groups["id"].Value, out attachmentId);
            //        if (attachmentId > 0 && !attachmentIds.Contains(attachmentId))
            //            attachmentIds.Add(attachmentId);
            //    }
            //}

            //IEnumerable<ContentAttachment> attachments = new ContentAttachmentService().Gets(attachmentIds);

            //if (attachments != null && attachments.Count() > 0)
            //{
            //    IList<BBTag> bbTags = new List<BBTag>();
            //    string htmlTemplate = "<div class=\"tn-annexinlaid\"><a href=\"{3}\" rel=\"nofollow\">{0}</a>（<em>{1}</em>,<em>下载次数：{2}</em>）<a href=\"{3}\" rel=\"nofollow\">下载</a> </div>";

            //    //解析文本中附件
            //    foreach (var attachment in attachments)
            //    {
            //        bbTags.Add(AddBBTag(htmlTemplate, attachment));
            //    }

            //    body = HtmlUtility.BBCodeToHtml(body, bbTags);
            //}

            //解析at用户
            AtUserService atUserService = new AtUserService(tenantTypeId);
            body = atUserService.ResolveBodyForDetail(body, associateId, userId, AtUserTagGenerate);

            AttachmentService attachmentService = new AttachmentService(tenantTypeId);
            IEnumerable<Attachment> attachments = attachmentService.GetsByAssociateId(associateId);
            if (attachments != null && attachments.Count() > 0)
            {
                IList<BBTag> bbTags = new List<BBTag>();
               string htmlTemplate = "<div class=\"tn-annexinlaid\"><a href=\"{3}\" rel=\"nofollow\">{0}</a>（<em>{1}</em>,<em>下载次数：{2}</em>）<a href=\"{3}\" rel=\"nofollow\">下载</a> </div>";

                //解析文本中附件
                IEnumerable<Attachment> attachmentsFiles = attachments.Where(n => n.MediaType != MediaType.Image);
                foreach (var attachment in attachmentsFiles)
                {
                    bbTags.Add(AddBBTag(htmlTemplate, attachment));
                }

                body = HtmlUtility.BBCodeToHtml(body, bbTags);
            }

            body = new EmotionService().EmoticonTransforms(body);
            body = DIContainer.Resolve<ParsedMediaService>().ResolveBodyForHtmlDetail(body, ParsedMediaTagGenerate);



            return body;
        }


        /// <summary>
        /// 生成at用户标签
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="displayName">显示名</param>
        private string AtUserTagGenerate(string userName, string displayName)
        {
            return string.Format("<a href=\"{1}\" target=\"_blank\" title=\"{0}\">@{0}</a> ", displayName, SiteUrls.Instance().SpaceHome(userName));
        }

        /// <summary>
        /// 添加BBTag实体
        /// </summary>
        /// <param name="htmlTemplate">html模板</param>
        /// <param name="attachment">带替换附件</param>
        /// <returns></returns>
        private BBTag AddBBTag(string htmlTemplate, Attachment attachment)
        {

            BBAttribute bbAttribute = new BBAttribute("attachTemplate", "",
                                                      n =>
                                                      {
                                                          return string.Format(htmlTemplate,
                                                                               attachment.FriendlyFileName,
                                                                               attachment.FriendlyFileLength,
                                                                               attachment.DownloadCount,
                                                                              SiteUrls.Instance().AttachmentUrl(attachment.AttachmentId,TenantTypeIds.Instance().ContentItem()));
                                                      },
                                                      HtmlEncodingMode.UnsafeDontEncode);

            return new BBTag("attach:" + attachment.AttachmentId, "${attachTemplate}", "", false, BBTagClosingStyle.LeafElementWithoutContent, null, bbAttribute);
        }

        /// <summary>
        /// 生成多媒体内容标签
        /// </summary>
        /// <param name="shrotUrl">短网址</param>
        /// <param name="parsedMedia">多媒体连接实体</param>
        private string ParsedMediaTagGenerate(string shrotUrl, ParsedMedia parsedMedia)
        {
            if (parsedMedia == null)
                return string.Empty;

            if (parsedMedia.MediaType == MediaType.Audio)
            {
                string musicHtml = "<p><a href=\"{0}\" ntype=\"mediaPlay\">{1}<span class=\"tn-icon tn-icon-music tn-icon-inline\"></span></a><br />"
                                   + "<a  href=\"{0}\" ntype=\"mediaPlay\" class=\"tn-button tn-corner-all tn-button-default tn-button-text-icon-primary\">"
                                   + "<span class=\"tn-icon tn-icon-triangle-right\"></span><span class=\"tn-button-text\">音乐播放</span></a></p>";
                return string.Format(musicHtml, SiteUrls.Instance()._MusicDetail(parsedMedia.Alias), shrotUrl);
            }
            else if (parsedMedia.MediaType == MediaType.Video)
            {
                string videoHtml = "<p><a  href=\"{0}\" ntype=\"mediaPlay\">{1}<span class=\"tn-icon tn-icon-movie tn-icon-inline\"></span></a><br />"
                                    + "<a ntype=\"mediaPlay\" href=\"{0}\"><img src=\"{2}\"></a></p>";
                return string.Format(videoHtml, SiteUrls.Instance()._VideoDetail(parsedMedia.Alias), shrotUrl, parsedMedia.ThumbnailUrl);
            }

            return string.Empty;
        }
    }
}