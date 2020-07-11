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
using System.Windows.Shapes;
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

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            this.txtCurrentFileName.Dispatcher.Invoke(DispatcherPriority.Normal,
     new Action(() =>
     {
         txtCurrentFileName.Text = e.FullPath;

     }));


        }
    }
}
