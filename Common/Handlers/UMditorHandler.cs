using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tunynet.Common;

namespace Spacebuilder.Common.Handlers
{
   public class UMditorHandler: IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            IUser user = UserContext.CurrentUser;
            if (user == null)
            {
                context.Response.Redirect(SiteUrls.Instance().Login());
            }
            MHandler action = null;
            switch (context.Request["action"])
            {
                case "config":
                    action = new MConfigHandler(context);
                    break;
                case "uploadimage":
                    action = new MUploadHandler(context, new MUploadConfig()
                    {
                        MAllowExtensions = MConfig.GetStringList("imageAllowFiles"),
                        MPathFormat = MConfig.GetString("imagePathFormat"),
                        MSizeLimit = MConfig.GetInt("imageMaxSize"),
                        MUploadFieldName = MConfig.GetString("imageFieldName")
                    });
                    break;
                case "uploadscrawl":
                    action = new MUploadHandler(context, new MUploadConfig()
                    {
                        MAllowExtensions = new string[] { ".png" },
                        MPathFormat = MConfig.GetString("scrawlPathFormat"),
                        MSizeLimit = MConfig.GetInt("scrawlMaxSize"),
                        MUploadFieldName = MConfig.GetString("scrawlFieldName"),
                        MBase64 = true,
                        MBase64Filename = "scrawl.png"
                    });
                    break;
                case "uploadvideo":
                    action = new MUploadHandler(context, new MUploadConfig()
                    {
                        MAllowExtensions = MConfig.GetStringList("videoAllowFiles"),
                        MPathFormat = MConfig.GetString("videoPathFormat"),
                        MSizeLimit = MConfig.GetInt("videoMaxSize"),
                        MUploadFieldName = MConfig.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    action = new MUploadHandler(context, new MUploadConfig()
                    {
                        MAllowExtensions = MConfig.GetStringList("fileAllowFiles"),
                        MPathFormat = MConfig.GetString("filePathFormat"),
                        MSizeLimit = MConfig.GetInt("fileMaxSize"),
                        MUploadFieldName = MConfig.GetString("fileFieldName")
                    });
                    break;
                case "listimage":
                    action = new MListFileManager(context, MConfig.GetString("imageManagerListPath"), MConfig.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    action = new MListFileManager(context, MConfig.GetString("fileManagerListPath"), MConfig.GetStringList("fileManagerAllowFiles"));
                    break;
                case "catchimage":
                    action = new MCrawlerHandler(context);
                    break;
                default:
                    action = new MNotSupportedHandler(context);
                    break;
            }
            action.Process();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
