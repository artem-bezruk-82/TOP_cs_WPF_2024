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
    /// Interaction logic for Window_Hello.xaml
    /// </summary>
    public partial class Window_Hello : Window
    {
        public Window_Hello()
        {
            InitializeComponent();
        }

        // обработчик для кнопки выхода на основе изображения
        private void Image_MouseDown_Exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        //обработчик для кнопки сворачивания на основе изображения
        private void Image_MouseDown_Minimize(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //обработчик для "Drag and Drop" передвижения окна(верхний ToolBar)
        private void ToolBar_MouseDown_DragDrop(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        //обработчик для "Drag and Drop" передвижения окна(среднее поле LogoContainer)
        private void LogoContainer_MouseDown_DragDrop(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window_SignIn window_SignIn = new Window_SignIn();
            window_SignIn.Show();
            this.Close();
        }
    }
}
