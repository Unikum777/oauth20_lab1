using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //UriBuilder uriBuilder = new UriBuilder(Request.UrlReferrer); 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            //FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> { { "username", Login.Text }, { "password", Password.Text } });
            FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> { 
            { "client_id", "000000004C12F126" }, 
            { "scope", "wl.signin%2cwl.basic" }, 
            {"response_type", "token" },
            { "redirect_uri", "www.ya.ru"} });

            /*Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(result);
            string token = (string)obj["access_token"];
            string refreshToken = (string)obj["refresh_token"];*/


            string oauth_webpage = "https://oauth.live.com/authorize";
            string client_id = "000000004C12F126";
            string scope = "wl.signin%2cwl.basic";
            string resp_type = "token";
            string redir = "http://www.mynewApplication25.test:45226/AccessTypeForm.aspx";


            var postData = "client_id=" + client_id;
            postData += "&scope=" + scope;
            postData += "&response_type=" + resp_type;
            postData += "&redirect_uri=" + redir;

            var request = (HttpWebRequest)WebRequest.Create(oauth_webpage + "?" + postData);


            //var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;
            /*using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }*/

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //Response.Redirect(responseString);
            Label3.Text = responseString;
        }
    }
}