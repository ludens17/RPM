using RPM.app;
using System;
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

namespace RPM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            connect.conObj = new agensEntities();
            FrameObj.MainFrame = MainFrame;
            MainFrame.Navigate(new Mainbutton());
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                btnback.Visibility = Visibility.Visible;
            }
            else
            {
                btnback.Visibility = Visibility.Hidden;
            }

        }
    }
}
