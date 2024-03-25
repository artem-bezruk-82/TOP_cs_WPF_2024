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
using System.Windows.Shapes;

namespace BugTracker.View
{
    /// <summary>
    /// Interaction logic for Window_AdminView.xaml
    /// </summary>
    public partial class Window_AdminView : Window
    {
        public Window_AdminView()
        {
            InitializeComponent();
        }

        private void MenuItem_TopMenuBar_Settings_Priorities_Click(object sender, RoutedEventArgs e)
        {
            Window_Priorities window_Priorities = new Window_Priorities();
            window_Priorities.Show();
        }



        private void MenuItem_TopMenuBar_Settings_Services_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_TopMenuBar_Settings_Statuses_Click(object sender, RoutedEventArgs e)
        {
            Window_Statuses window_Statuses = new Window_Statuses();
            window_Statuses.Show();
        }

        private void MenuItem_TopMenuBar_Settings_Types_Click(object sender, RoutedEventArgs e)
        {
            Window_Types window_Types = new Window_Types();
            window_Types.Show();
        }
    }
}
