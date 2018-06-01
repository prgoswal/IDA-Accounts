using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SendRequest
/// </summary>
public sealed class SendRequest
{
    public static string SendMessage(string MobileNo, string Message)
    {
        try
        {
            string SenderID = "SAATHI";
            StringBuilder sbpostdata = new StringBuilder();
            sbpostdata.AppendFormat("&mobiles={0}", MobileNo);
            sbpostdata.AppendFormat("&message={0}", Message);
            sbpostdata.AppendFormat("&sender={0}", SenderID);
            sbpostdata.AppendFormat("&route={0}", "4");
            string sendSMSUri = "";
            sendSMSUri = "http://msg.msgclub.net/sendhttp.php?authkey=413Azi1FBPp532d30e3";
            //sendSMSUri = "http://panel.msgclub.net/user/index.php?authkey=413Azi1FBPp532d30e3";
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbpostdata.ToString());
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse Response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string ResponseString = Reader.ReadToEnd();
            Reader.Close();
            Response.Close();
            return ResponseString;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static void SendHtmlFormattedEmail(string body, string email)
    {
        try
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                MailMessage loginInfo = new MailMessage();
                loginInfo.To.Add(email);

                loginInfo.From = new MailAddress("gst@gstsaathi.com");
                loginInfo.Subject = "GST SAATHI";
                loginInfo.Body = body;
                loginInfo.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.yandex.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("gst@gstsaathi.com", "saathi123");
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(loginInfo);
            }
        }
        catch (System.Net.Mail.SmtpException ex)
        {
           HttpContext.Current.Response.Write(ex.Message);
        }
    }

}