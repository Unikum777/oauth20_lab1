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
        int i = 0;
        WebClient client2 = new WebClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            //auth3();
            get_table();
        }
        private void auth3()
        {
            //string m = Request.QueryString["ID"];
            var m = Request.Url.Fragment;
            var t = Request.Params;
            HttpClient client = new HttpClient();

            string oauth_webpage = "https://oauth.live.com/authorize";
            string client_id = "000000004C12F126";
            string scope = "wl.signin%2cwl.basic";
            string resp_type = "token";
            string redir = "http://mymailru99658.ru/myapp/";

            var postData = "client_id=" + client_id;
            postData += "&scope=" + scope;
            postData += "&response_type=" + resp_type;
            postData += "&redirect_uri=" + redir;

            var request = (HttpWebRequest)WebRequest.Create(oauth_webpage + "?" + postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;

           /* var scUri = new Uri(oauth_webpage);
            
            client2.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
            client2.UploadStringAsync(scUri, HttpUtility.UrlEncode(postData));*/

            var response = (HttpWebResponse)request.GetResponse();
             var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
             Label3.Text = responseString;
        }
        private void get_table()
        {
            string _Token = "EwAwAq1DBAAUGCCXc8wU%2fzFu9QnLdZXy%2bYnElFkAAfwlU92oteWrUL%2fF3FudJvlRkSl9ZtpfGOdsbbAGukemUurnx5PujF0%2b%2fkFoUHBaAXM9TN0YqyST33NeCtIp69fAqpU81IINQtp8Z2Kvw9yzcpwxV0YOkNbsxF3nug6c5dIalR5oWLhA1ZWCCjP21SfLOAx1JQmJ4j0Rg2%2bQjZgMt8eFV87cJGDzRmFmz1IVt16zlVLN9SE5BP07wAu4X%2bZesLEVN94DNwVmVpM5lO82EcKr0wyAJO%2bUZu863YHPxHXPnMvNi%2bE2e5hnEh1Y4z%2fqwikGMlTv0LDrHz5m8y%2fatyqi758lPnh3DFMPmPj97CMt230nF229XnMg63Sj124DZgAACEvrZKceNC7zAAEQPnRPtbGvJzu7e%2fjyl6P9A5m2VKr09a44Y5u%2fq55LBSmtEsEQMqhOGIZaWn8RnAVD%2b%2ba8FEMDgWrtwKspqbyhkbJB1IcOKKrjT90imMLPku7h4zc3O9qYlchV%2fyWbwx1NCE5e6hWky8yKsNVmrqmc5L3r%2bzAFIL3vc1hUhobWdI6FCkBfs1nYZd87mbPgFgfwtVFNT6RSRgb6X3kC6xNL9uuDz4hCv88m20vVnS52AVeoM98B5q1aEwcDH7joEsq%2b8AWDMmIgExgfo5UacGbAw%2b8rOx%2bcqKNnjHho3VlAX87KeAcfMp5gD3fMYPsp%2bteTsaEJ1AVzlzcR3KTkiqXGAAA%3d";
            var _Url = "https://apis.live.net/v5.0/me?access_token={0}";
            _Url = string.Format(_Url, HttpUtility.UrlEncode(_Token));

            var request = (HttpWebRequest)WebRequest.Create(_Url);

            request.Method = "GET";
            request.ContentType = "text/xml; encoding='utf-8'";

            /* var scUri = new Uri(oauth_webpage);
            
             client2.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
             client2.UploadStringAsync(scUri, HttpUtility.UrlEncode(postData));*/

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Label3.Text = responseString;
        }
        private void auth2()
        {
            i++;
            var _queryString = new Dictionary<string, string>
            {
                 { "client_id", "000000004C12F126&#39" }, 
                 {"response_type", "token" },
                 { "scope", "wl.signin,wl.basic,wl.offline_access" }, 
                 { "redirect_uri", "http://mymailru99658.ru/myapp/"} 

            }.Select(x => x.Key + "=" + HttpUtility.UrlEncode(x.Value));

            var _Url = "https://oauth.live.com/authorize?" +
                string.Join("&", _queryString.ToArray<string>());
            var request = (HttpWebRequest)WebRequest.Create(_Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;
            request.AllowAutoRedirect = true;
            var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Response.Redirect(_Url);

        }

        void client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Error == null)
            {
                Label3.Text = "Succeeded!";
                string tokenInfo = e.Result.ToString();
               // MessageBox.Show(tokenInfo);
                // parse access token
                tokenInfo = tokenInfo.Remove(0, tokenInfo.IndexOf("token\":\"") + 8);
                string token = tokenInfo.Remove(tokenInfo.IndexOf("\""));

                // SoundCloud API get request
                string soundCloudMeReq = "https://api.soundcloud.com/me";
                //string soundCloudMeReq = "https://api.soundcloud.com/me/connections.json";
                var scUri = new Uri(soundCloudMeReq + "?oauth_token=" + token);
                client2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client2.DownloadStringAsync(scUri);
            }
            else
            {
                //lblError.Content = e.Error.ToString();
                Label3.Text = "Wrong login or password!";
            }
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Error == null)
            {
                string meData = e.Result;
                //MessageBox.Show(meData);
            }
            else
            {
                //lblError.Content = e.Error.ToString();
                Label3.Text = "Some problem with connecting to server!";
            }
        }
        private void auth1()
        {
            //string m = Request.QueryString["ID"];
            var m = Request.Url.Fragment;
            var t = Request.Params;
            HttpClient client = new HttpClient();
            //FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> { { "username", Login.Text }, { "password", Password.Text } });
            FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> { 
            { "client_id", "000000004C12F126" }, 
            { "scope", "wl.signin%2cwl.basic" }, 
            {"response_type", "token" },
            { "redirect_uri", "https://oauth.live.com/desktop"} });

            /*Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(result);
            string token = (string)obj["access_token"];
            string refreshToken = (string)obj["refresh_token"];*/


            string oauth_webpage = "https://oauth.live.com/authorize";
            string client_id = "000000004C12F126";
            string scope = "wl.signin%2cwl.basic";
            string resp_type = "token";
            //string redir = "http://www.mynewApplication25.test:45226/AccessTypeForm.aspx";
            string redir = "http://mymailru99658.ru/myapp/";

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
        private void requestanalyze()
        {
            var m = Request.Url.Fragment;
            var t = Request.Params;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            var m = Request.Url.Fragment;
            var t = Request.Params;
        }
    }
}