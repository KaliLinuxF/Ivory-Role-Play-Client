using Microsoft.Win32;
using System.Windows;
using System;
using System.Windows.Threading;
using AngleSharp;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Net.Http;

namespace Ivory_Role_Play_Client
{
  
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string Path = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0,0,1);
            timer.Start();

         
        }

        private async void timer_Tick(object sender, EventArgs e)
        {

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync("https://toplay.su/server/view/3182");
            var pricesListItemsLinq = document.QuerySelectorAll("p.online-players");
            string edit = string.Empty;

            foreach (var item in pricesListItemsLinq)
            {
                edit = item.TextContent;
            }

            string[] words = edit.Split(' ');

            lPlayers.Content = words[2] + words[3] + words[4];


            sPlayers.Value = Convert.ToInt32(words[2]);


        }

        private void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://i-rp.ru/donate");
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
           Application.Current.Shutdown();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = openFileDialog.FileName.ToString();

                    using (var profilesKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SAMP",true))
                    {
                        profilesKey.SetValue("sampexe", filePath);
                    }

                }
            }
        }
        

        private void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
     
        }

        private void BtnYouTube_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC-D8z6LkPaiZ2n-pK-ZsAFg");
        }

        private void BtnVK_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/ivoryroleplaye");
        }

        private void BtnDiscord_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/ggeywTb");
        }

       

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if(tbNick.Text != null)
            {
                using (var profilesKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SAMP",true))
                {

                    if(profilesKey.GetValue("sampexe") == null)
                    {
                        MessageBox.Show("Укажите папку с игрой в настройках!");
                    }

                    profilesKey.SetValue("PlayerName", tbNick.Text);
                    System.Diagnostics.Process.Start(Path, "176.32.39.69:7777");
                }
            }
            else
            {
               MessageBox.Show("Введите ник!");
            }
        }

        private void TbNick_Loaded(object sender, RoutedEventArgs e)
        {
            using (var profilesKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SAMP"))
            {
                tbNick.Text = profilesKey.GetValue("PlayerName").ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var profilesKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SAMP"))
            {
                Path = profilesKey.GetValue("sampexe").ToString();
            }
        }
    }
}
