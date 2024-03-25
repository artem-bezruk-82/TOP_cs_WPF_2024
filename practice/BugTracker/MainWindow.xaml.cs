using BugTracker.View;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Window_Hello window_Hello = new Window_Hello();
            //window_Hello.Show();

            //Window_AdminView adminView = new Window_AdminView();
            //adminView.Show();

            Window_Types window_Types = new Window_Types();
            window_Types.Show();

            //Window_SignIn window_SignIn = new Window_SignIn();
            //window_SignIn.Show();

            this.Close();
        }
    }
}