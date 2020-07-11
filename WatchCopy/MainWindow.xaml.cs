using System;
using System.Collections.Generic;
using System.IO;
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
//using System.Windows.Shapes;
using System.Windows.Threading;

namespace WatchCopy
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        FileSystemWatcher watcher = new FileSystemWatcher();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            if (DateTime.Now > new DateTime(2020, 7, 13))
                Application.Current.Shutdown();
#endif
            this.txtCurrentDirName.Text = Environment.CurrentDirectory;
            watcher.Path = Environment.CurrentDirectory;
            watcher.Created += Watcher_Created;
            watcher.EnableRaisingEvents = true;
        }

        private void CopyFile(string srcFullName)
        {
            string srcDir = Path.GetDirectoryName(srcFullName);
            //string fnWithoutExt = Path.GetFileNameWithoutExtension(srcFullName);
            string ext = Path.GetExtension(srcFullName);
            string desFullName = srcFullName.Replace(ext, ".bak" + ext);
            if (!File.Exists(desFullName))
            {
                try
                {
                    File.Copy(srcFullName, desFullName);
                }
                catch (Exception ex)
                {
                    this.txtMsg.Dispatcher.Invoke(DispatcherPriority.Normal,
new Action(() =>
{
    this.txtMsg.Text = ex.Message;
}));

                }
            }
        }




        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string srcFullName = e.FullPath;
            if (srcFullName.Contains(".bak."))
                return;

            this.txtCurrentFileName.Dispatcher.Invoke(DispatcherPriority.Normal,
     new Action(() =>
     {
         txtCurrentFileName.Text = srcFullName;
     }));
            CopyFile(srcFullName);

        }
    }
}
