using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tunynet.Common;
using Tunynet.UI;
using Com.Alipay;
using System.Collections.Specialized;



namespace Spacebuilder.Common.Controllers
{
    [Themed(PresentAreaKeysOfBuiltIn.Channel, IsApplication = false)]
    public class AlipayController : Controller
    {
        private string OrderNumber;//订单号
        private string OrderName;//订单名称
        private string OrderMoney;//订单金额
        private string OrderDescribe;//订单描述
        private string ProductExhibitionUrl;//商品展示网址
        /// <summary>
        /// 支付宝确认支付页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PayPage()
        {
            //string bianma = Tunynet.Utilities.EncryptionUtility.Base64_Encode("OrderNumber=2015072217392533&OrderName=12312312&OrderMoney=0.01&OrderDescribe=好看&ProductExhibitionUrl=http://www.baidu.com");
            //string PayUrl = Request.QueryString["PayUrl"];


            //string decode = Tunynet.Utilities.EncryptionUtility.Base64_Decode(PayUrl);
            //string[] NameArr = decode.Split('&');
            //string[] OrderInfo = new string[5] { "", "", "", "", "" };
            //for (int i = 0; i < NameArr.Length; i++)
            //{
            //    string MyValue = NameArr[i];
            //    int IPos = MyValue.IndexOf("=");
            //    string Words = MyValue.Substring(IPos + 1);
            //    OrderInfo[i] = Words;
            //}

            //string jiema = Tunynet.Utilities.EncryptionUtility.Base64_Decode("T3JkZXJOYW1lPTEyMzEyMzEyJk9yZGVyTW9uZXk9MTIzMTIzMSZPcmRlck51bWJlcj0yMDE1MDcyMjE3MzkyNTMzJk9yZGVyRGVzY3JpYmU95aW955yLJlByb2R1Y3RFeGhpYml0aW9uVXJsPWh0dHA6Ly93d3cuYmFpZHUuY29t");

            //ViewData["OrderNumber"] = OrderInfo[0];
            //ViewData["OrderName"] = OrderInfo[1];
            //ViewData["OrderMoney"] = OrderInfo[2];
            //ViewData["OrderDescribe"] = OrderInfo[3];
            //ViewData["ProductExhibitionUrl"] = OrderInfo[4];
            return View();
        }

        /// <summary>
        /// 点击确认后的处理函数
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PayPages(FormCollection collection)
        {
            OrderNumber = collection["WIDout_trade_no"];
            OrderName = collection["WIDsubject"];
            OrderMoney = collection["WIDtotal_fee"];
            OrderDescribe = collection["WIDbody"];
            ProductExhibitionUrl = collection["WIDshow_url"];

            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = "http://demo.jinhusns.com/Alipay/notify_url";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = "http://demo.jinhusns.com/Alipay/return_url";
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //商户订单号
            string out_trade_no = OrderNumber.Trim();
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = OrderName.Trim();
            //必填

            //付款金额
            string total_fee = OrderMoney.Trim();
            //必填

            //订单描述

            string body = OrderDescribe.Trim();
            //商品展示地址
            string show_url = ProductExhibitionUrl.Trim();
            //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Configs.Partner);
            sParaTemp.Add("seller_email", Configs.Seller_email);
            sParaTemp.Add("_input_charset", Configs.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            HtmlString url = new HtmlString(sHtmlText);
            
            ViewBag.url = url;
            //Response.Write(sHtmlText);


            return View("PayPage");
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult notify_url()
        //{
        //    SortedDictionary<string, string> sPara = GetRequestPost();

        //    if (sPara.Count > 0)//判断是否有带返回参数
        //    {
        //        Notify aliNotify = new Notify();
        //        bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

        //        if (verifyResult)//验证成功
        //        {
        //            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //            //请在这里加上商户的业务逻辑程序代码


        //            //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
        //            //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

        //            //商户订单号

        //            string out_trade_no = Request.Form["out_trade_no"];

        //            //支付宝交易号

        //            string trade_no = Request.Form["trade_no"];

        //            //交易状态
        //            string trade_status = Request.Form["trade_status"];


        //            if (Request.Form["trade_status"] == "TRADE_FINISHED")
        //            {
        //                //判断该笔订单是否在商户网站中已经做过处理
        //                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                //如果有做过处理，不执行商户的业务程序

        //                //注意：
        //                //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
        //            }
        //            else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
        //            {
        //                //判断该笔订单是否在商户网站中已经做过处理
        //                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
        //                //如果有做过处理，不执行商户的业务程序

        //                //注意：
        //                //付款完成后，支付宝系统发送该交易状态通知
        //            }
        //            else
        //            {
        //            }

        //            //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

        //            Response.Write("success");  //请不要修改或删除

        //            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        }
        //        else//验证失败
        //        {
        //            Response.Write("fail");
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("无通知参数");
        //    }

        //    return View();
        //}

        public ActionResult return_url()
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码




                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.QueryString["out_trade_no"];

                    //支付宝交易号

                    string trade_no = Request.QueryString["trade_no"];
                    //获取总金额  
                    string total_fee = Request.QueryString["total_fee"]; 
                    //交易状态
                    string trade_status = Request.QueryString["trade_status"];


                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {

                        return RedirectToAction("TradeSuccess", "ChannelEvent", new RouteValueDictionary { { "area", "Event" }, { "orderNumber", out_trade_no }});
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                    }
                    else
                    {
                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }

                    //打印页面
                    Response.Write("验证成功<br />");

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("验证失败");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }

            return View();
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

    }
}
