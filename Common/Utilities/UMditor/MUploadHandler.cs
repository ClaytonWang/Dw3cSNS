using Spacebuilder.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Tunynet;
using Tunynet.Common;
using Tunynet.Common.Configuration;
using Tunynet.FileStore;

/// <summary>
/// MUploadHandler 的摘要说明
/// </summary>
public class MUploadHandler : MHandler
{

    public MUploadConfig MUploadConfig { get; private set; }
    public MUploadResult Result { get; private set; }

    public MUploadHandler(HttpContext context, MUploadConfig config)
        : base(context)
    {
        this.MUploadConfig = config;
        this.Result = new MUploadResult() { MState = MUploadState.MUnknown };
    }

    public override void Process()
    {
        byte[] uploadFileBytes = null;
        string uploadFileName = null;

        if (MUploadConfig.MBase64)
        {
            uploadFileName = MUploadConfig.MBase64Filename;
            uploadFileBytes = Convert.FromBase64String(Request[MUploadConfig.MUploadFieldName]);
        }
        else
        {
            var file = Request.Files[MUploadConfig.MUploadFieldName];
            uploadFileName = file.FileName;

            if (!CheckFileType(uploadFileName))
            {
                Result.MState = MUploadState.MTypeNotAllow;
                WriteResult();
                return;
            }
            if (!CheckFileSize(file.ContentLength))
            {
                Result.MState = MUploadState.MSizeLimitExceed;
                WriteResult();
                return;
            }

            uploadFileBytes = new byte[file.ContentLength];
            try
            {
                file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
            }
            catch (Exception)
            {
                Result.MState = MUploadState.MNetworkError;
                WriteResult();
            }
        }

        IUser user = UserContext.CurrentUser;
        string tenantTypeId = Request["tenantTypeId"];
        bool resize = true;
        long associateId = 0;
        long.TryParse(Context.Request["associateId"], out associateId);
        AttachmentService<Attachment> attachementService = new AttachmentService<Attachment>(tenantTypeId);
        long userId = user.UserId, ownerId = user.UserId;
        string userDisplayName = user.DisplayName;
        Result.MOriginFileName = uploadFileName;

        try
        {
            //保存文件
            string contentType = MimeTypeConfiguration.GetMimeType(uploadFileName);
            MemoryStream ms = new MemoryStream(uploadFileBytes);
            string friendlyFileName = uploadFileName.Substring(uploadFileName.LastIndexOf("\\") + 1);
            Attachment attachment = new Attachment(ms, contentType, friendlyFileName);           
            attachment.UserId = userId;
            attachment.AssociateId = associateId;
            attachment.TenantTypeId = tenantTypeId;
            attachment.OwnerId = ownerId;
            attachment.UserDisplayName = userDisplayName;
            attachementService.Create(attachment, ms);
            ms.Dispose();
            Result.MUrl = SiteUrls.Instance().AttachmentDirectUrl(attachment);
            Result.MState = MUploadState.MSuccess;
        }
        catch (Exception e)
        {
            Result.MState = MUploadState.MFileAccessError;
            Result.MErrorMessage = e.Message;
        }
        finally
        {
            WriteResult();
        }
    }

    private void WriteResult()
    {
        this.WriteJson(new
        {
            state = GetStateMessage(Result.MState),
            url = Result.MUrl,
            title = Result.MOriginFileName,
            original = Result.MOriginFileName,
            error = Result.MErrorMessage
        });
    }

    private string GetStateMessage(MUploadState state)
    {
        switch (state)
        {
            case MUploadState.MSuccess:
                return "SUCCESS";
            case MUploadState.MFileAccessError:
                return "文件访问出错，请检查写入权限";
            case MUploadState.MSizeLimitExceed:
                return "文件大小超出服务器限制";
            case MUploadState.MTypeNotAllow:
                return "不允许的文件格式";
            case MUploadState.MNetworkError:
                return "网络错误";
        }
        return "未知错误";
    }

    private bool CheckFileType(string filename)
    {
        var fileExtension = Path.GetExtension(filename).ToLower();
        return MUploadConfig.MAllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
    }

    private bool CheckFileSize(int size)
    {
        return size < MUploadConfig.MSizeLimit;
    }
}

public class MUploadConfig
{
    /// <summary>
    /// 文件命名规则
    /// </summary>
    public string MPathFormat { get; set; }

    /// <summary>
    /// 上传表单域名称
    /// </summary>
    public string MUploadFieldName { get; set; }

    /// <summary>
    /// 上传大小限制
    /// </summary>
    public int MSizeLimit { get; set; }

    /// <summary>
    /// 上传允许的文件格式
    /// </summary>
    public string[] MAllowExtensions { get; set; }

    /// <summary>
    /// 文件是否以 Base64 的形式上传
    /// </summary>
    public bool MBase64 { get; set; }

    /// <summary>
    /// Base64 字符串所表示的文件名
    /// </summary>
    public string MBase64Filename { get; set; }
}

public class MUploadResult
{
    public MUploadState MState { get; set; }
    public string MUrl { get; set; }
    public string MOriginFileName { get; set; }

    public string MErrorMessage { get; set; }
}

public enum MUploadState
{
    MSuccess = 0,
    MSizeLimitExceed = -1,
    MTypeNotAllow = -2,
    MFileAccessError = -3,
    MNetworkError = -4,
    MUnknown = 1,
}

