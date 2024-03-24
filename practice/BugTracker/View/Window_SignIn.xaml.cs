using BugTracker.DataModel;
using BugTracker.Present;
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
    /// Interaction logic for Window_SignIn.xaml
    /// </summary>
    public partial class Window_SignIn : Window
    {
        private UserLogin? userLogin;
        private OperationEnum currentOperationEnum;

        public Window_SignIn()
        {
            InitializeComponent();
            userLogin = new UserLogin();
            currentOperationEnum = OperationEnum.None;
        }

        enum OperationEnum 
        {
            None,
            Login,
            Password
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

        private void OnPasswordChanged(object sender, EventArgs e)
        {
            //if (passTB_RPG.Password.Length > 0)
            //{
            //    passWatermark.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    passWatermark.Visibility = Visibility.Visible;
            //}
        }

        private void OnRepeatPasswordChanged(object sender, EventArgs e)
        {
            //if (repeatPassTB_RPG.Password.Length > 0)
            //{
            //    repeatPassWatermark.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    //repeatPassWatermark.Visibility = Visibility.Visible;
            //}
        }

        private void textBox_Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentOperationEnum == OperationEnum.Login)
            {
                UserLogin userLoginFromBD = Presenter.GetUserLogin(textBox_Login.Text.ToLower());
                if (userLoginFromBD is not null)
                {
                    userLogin = userLoginFromBD;
                    passwordBox.Visibility = Visibility.Visible;
                    currentOperationEnum = OperationEnum.Password;
                }
                else
                {
                    MessageBox.Show("Thre is not such login");
                }
            }
            else if (currentOperationEnum == OperationEnum.Password) 
            {
                if (passwordBox.Password == userLogin?.LoginsPassword?.Password) 
                {
                    MessageBox.Show("Open next window");
                } 
                else 
                {
                    MessageBox.Show("Login and password do not match");
                }
            }

        }


    }
}
