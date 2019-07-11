using Microsoft.Win32;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System;

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
        }

        private void BtnStore_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://i-rp.ru/donate");
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
                        System.Windows.MessageBox.Show("Укажите папку с игрой в настройках!");
                    }

                    profilesKey.SetValue("PlayerName", tbNick.Text);
                    System.Diagnostics.Process.Start(Path, "176.32.39.69:7777");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Введите ник!");
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
