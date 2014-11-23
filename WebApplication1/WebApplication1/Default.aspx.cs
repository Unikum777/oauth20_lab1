using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            //FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> { { "username", Login.Text }, { "password", Password.Text } });
            FormUrlEncodedContent form = new FormUrlEncodedContent(new Dictionary<string, string> { 
            { "client_id", "000000004C12F126" }, 
            { "scope", "huh9wvXlYFQ94hJ198i-7bOnBn46oi4a" }, 
            { "redirect_uri", "http://localhost:45226/Default.aspx"} });

            var message = client.PostAsync("https://login.live.com/oauth20_authorize.srf", form);
            String result = message.Result.Content.ReadAsStringAsync().Result;

            Label3.Text = result;
            /*Newtonsoft.Json.Linq.JObject obj = Newtonsoft.Json.Linq.JObject.Parse(result);
            string token = (string)obj["access_token"];
            string refreshToken = (string)obj["refresh_token"];*/
        }
    }
}