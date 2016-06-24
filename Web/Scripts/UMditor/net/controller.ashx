<%@ WebHandler Language="C#" Class="UEditorHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

public class UEditorHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        MHandler action = null;
        switch (context.Request["action"])
        {
            case "config":
                action = new MConfigHandler(context);
                break;
            case "uploadimage":
                action = new MUploadHandler(context, new MUploadConfig()
                {
                    MAllowExtensions = Config.GetStringList("imageAllowFiles"),
                    MPathFormat = Config.GetString("imagePathFormat"),
                    MSizeLimit = Config.GetInt("imageMaxSize"),
                    MUploadFieldName = Config.GetString("imageFieldName")
                });
                break;
            case "uploadscrawl":
                action = new MUploadHandler(context, new MUploadConfig()
                {
                    MAllowExtensions = new string[] { ".png" },
                    MPathFormat = Config.GetString("scrawlPathFormat"),
                    MSizeLimit = Config.GetInt("scrawlMaxSize"),
                    MUploadFieldName = Config.GetString("scrawlFieldName"),
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