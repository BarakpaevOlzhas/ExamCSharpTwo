using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace ConAsync
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread[] threads;
        int[] mas;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClickStart(object sender, RoutedEventArgs e)
        {
            int result;
            if (int.TryParse(textBoxCount.Text, out result))
            {
                threads = new Thread[result];
                mas = new int[result];
                for (int i = 0; i < threads.Length; i++) 
                {
                    threads[i] = new Thread(WriteMass);
                }

                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Start(i);
                }

                foreach (var i in mas)
                {
                    textBoxTest.Text += i + " ";
                    Thread.Sleep(1);
                }
            }

        }

        private void WriteMass(object count)
        {            
            mas[(int)count] = (int)count + 1;            
        }

        private void ButtonClickDownload(object sender, RoutedEventArgs e)
        {
            DownloadFile(textBoxUrlPathDownload.Text, textBoxName.Text);
            SetDataBase(textBoxUrlPathDownload.Text, textBoxName.Text);
        }

        private async void DownloadFile(string url, string name)
        {
            await Task.Run(() => {
                WebClient wc = new WebClient();

                wc.DownloadFile(url, name);
            });                        
        }

        private async void SetDataBase(string url, string name)
        {
            await Task.Run(() => {
                FileInfo f = new FileInfo(name);
                using (var context = new FileContext())
                {
                    context.Files.Add(new File
                    {
                        FullPath = url,
                        PathInExplorer = f.FullName
                    });

                    context.SaveChanges();
                }
            });
        }


    }
}
