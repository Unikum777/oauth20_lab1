﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

namespace DesktopDemo
{
	
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		static string client_id = "0000000048135423";
		static string client_secret = "Sq4GoSUbQTgeRFyNc5cgbcVROHVW1XOP";
		static string accessTokenUrl = String.Format(@"https://login.live.com/oauth20_token.srf?client_id={0}&client_secret={1}&redirect_uri=https://login.live.com/oauth20_desktop.srf&grant_type=authorization_code&code=", client_id, client_secret);
		static string apiUrl = @"https://apis.live.net/v5.0/";
		public Dictionary<string, string> tokenData = new Dictionary<string, string>();
		
		public MainWindow()
		{
			InitializeComponent();
		}
		private void getAccessToken()
		{
			if (App.Current.Properties.Contains("auth_code"))
			{
				makeAccessTokenRequest(accessTokenUrl + App.Current.Properties["auth_code"]);
			}
		}

		private void makeAccessTokenRequest(string requestUrl)
		{
			WebClient wc = new WebClient();
			wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(accessToken_DownloadStringCompleted);
			wc.DownloadStringAsync(new Uri(requestUrl));
		}

		void accessToken_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			tokenData = deserializeJson(e.Result);
			if (tokenData.ContainsKey("access_token"))
			{
				App.Current.Properties.Add("access_token", tokenData["access_token"]);
				getUserInfo();
			}
		}

		private Dictionary<string, string> deserializeJson(string json)
		{
			var jss = new JavaScriptSerializer();
			var d = jss.Deserialize<Dictionary<string, string>>(json);
			return d;
		}

		private void getUserInfo()
		{
			if (App.Current.Properties.Contains("access_token"))
			{
                makeApiRequest(apiUrl + "me/skydrive/files?access_token=" + App.Current.Properties["access_token"]);
			}
		}

		private void makeApiRequest(string requestUrl)
		{
			WebClient wc = new WebClient();
			wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
			wc.DownloadStringAsync(new Uri(requestUrl));
		}

		void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			changeView(e.Result);
		}
		private void changeView(string result)
		{
			btnSignIn.Visibility = Visibility.Collapsed;
            

            
            dataBrowser.NavigateToString(result);
			//txtUserInfo.Text = result;
			string imgUrl = apiUrl + "me/picture?access_token=" + App.Current.Properties["access_token"];
			imgUser.Source = new BitmapImage(new Uri(imgUrl, UriKind.RelativeOrAbsolute));
			txtToken.Text += "access_token = " + App.Current.Properties["access_token"] + "\r\n\r\n";
		}
		private void btnSignIn_Click(object sender, RoutedEventArgs e)
		{
			BrowserWindow browser = new BrowserWindow();
			browser.Closed += new EventHandler(browser_Closed);
			browser.Show();
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			App.Current.Properties.Clear();
			btnSignIn.Visibility = Visibility.Visible;
			txtToken.Text = "";
			imgUser.Source = null;
			//txtUserInfo.Text = "";
		}
		void browser_Closed(object sender, EventArgs e)
		{
			getAccessToken();
		}

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
