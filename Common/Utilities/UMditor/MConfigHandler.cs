using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// MConfigHandler 的摘要说明
/// </summary>
public class MConfigHandler : MHandler
{
    public MConfigHandler(HttpContext context) : base(context) { }

    public override void Process()
    {
        WriteJson(MConfig.Items);
    }
}