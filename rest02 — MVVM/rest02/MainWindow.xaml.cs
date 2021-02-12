using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using rest02.ModelWidoku;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Forms;
using System.Windows.Shapes;

namespace rest02
{

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Client1 client = new Client1();
        string[] files;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
             if(e.Data.GetDataPresent(DataFormats.FileDrop))
             {
                 files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ((Client1)this.DataContext).post(files);
             }
        }
    }
}
