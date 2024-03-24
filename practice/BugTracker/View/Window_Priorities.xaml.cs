using BugTracker.DataModel;
using BugTracker.Present;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace BugTracker.View
{
    /// <summary>
    /// Interaction logic for Window_Priorities.xaml
    /// </summary>
    public partial class Window_Priorities : Window
    {
        private delegate void ErrorDetection (object sender, bool errorDetected, string errorMessage);
        private event ErrorDetection errorDetection;

        private enum CurrentOperationsEnum 
        {
            None,
            Add,
            Edit,
            Remove
        }

        private CurrentOperationsEnum currentOperation;
        private TicketPriority currentPriority;
        private ObservableCollection<TicketPriority> ticketPriorities;

        public string? watermarkText 
        {
            get 
            {
                return currentOperation switch
                {
                    CurrentOperationsEnum.Add => "Please enter priority name",
                    CurrentOperationsEnum.Edit => currentPriority?.Name,
                    _ => string.Empty,
                };
            } 
        }

        public Window_Priorities()
        {
            currentPriority = new TicketPriority();
            ticketPriorities = new ObservableCollection<TicketPriority>();
            currentOperation = CurrentOperationsEnum.None;
            InitializeComponent();
            LoadDataFromDB();
            ResetControls();
            errorDetection += Window_Priorities_errorDetection;
        }

        private void InitializeMembers() 
        {
            currentOperation = CurrentOperationsEnum.None;
            currentPriority = new TicketPriority();
        }

        private void ResetControls() 
        {
            textBox_UserInput.Clear();
            textBox_UserInput.Visibility = Visibility.Collapsed;

            button_Save.Visibility = Visibility.Collapsed;
            button_Cancel.Visibility = Visibility.Collapsed;

            button_Add.Visibility = Visibility.Visible;
            button_Edit.Visibility = Visibility.Collapsed;
            button_Remove.Visibility = Visibility.Collapsed;
            
            listBox_Priorities.ItemsSource = ticketPriorities;
        }

        private void LoadDataFromDB() 
        {
            ticketPriorities = new ObservableCollection<TicketPriority>(Presenter.GetTicketPriorities());
        }

        private void RefreshWindow() 
        {
            InitializeMembers();
            LoadDataFromDB();
            ResetControls();
        }

        private void Window_Priorities_errorDetection(object sender, bool errorDetected, string errorMessage)
        {
            if (sender is Control control)
            {
                control.ToolTip = errorDetected ? errorMessage : string.Empty;
                control.BorderBrush = errorDetected ? Brushes.Red : Brushes.Black;
                control.Foreground = errorDetected ? Brushes.Red : Brushes.Black;
            }
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            currentOperation = CurrentOperationsEnum.Add;
            textBox_Watermark.Text = watermarkText;
            textBox_UserInput.Visibility = Visibility.Visible;
            button_Cancel.Visibility = Visibility.Visible;
            button_Add.Visibility = Visibility.Collapsed;
            button_Edit.Visibility = Visibility.Collapsed;
            button_Remove.Visibility = Visibility.Collapsed;

        }

        private void textBox_UserInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorDetection.Invoke(textBox_UserInput, false, string.Empty);

            button_Save.Visibility = Visibility.Visible;
            
            textBox_Watermark.Visibility = (string.IsNullOrEmpty(textBox_UserInput.Text))
                ? Visibility.Visible : Visibility.Collapsed;
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            currentOperation = CurrentOperationsEnum.None;
            currentPriority = new TicketPriority();
            ResetControls();
        }

        private (bool isValid, string message) TextIsValid() 
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(textBox_UserInput.Text)) 
            {
                msg = "Input is empty";
                return (false, msg);
            }
            if (ticketPriorities.Any(p => p.Name.ToLower() == textBox_UserInput.Text.ToLower()))
            {
                msg = "Entry already exists";
                return (false, msg);
            }
            return (true, msg);
        }

        private void TryToAdd() 
        {
            var added = Presenter.AddToDB(currentPriority);
            if (!added.result) 
            {
                string msg = $"Entry [{currentPriority?.Name}] adding failed:\n{added.message}";
                MessageBox.Show(msg);
                errorDetection.Invoke(textBox_UserInput, true, msg);
            }
            else 
            {
                MessageBox.Show($"Entry [{currentPriority?.Name}] added successfully ");
            }
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            textBox_UserInput.Text = textBox_UserInput.Text.Trim();
            var textValidation = TextIsValid();
            if (textValidation.isValid) 
            {
                switch (currentOperation) 
                {
                    case CurrentOperationsEnum.Add:
                        currentPriority.Name = textBox_UserInput.Text;
                        TryToAdd();
                        break;
                    case CurrentOperationsEnum.Edit:
                        TryToUpdate();
                        break;
                }
                RefreshWindow();
            }
            else 
            {
                errorDetection.Invoke(textBox_UserInput, true, textValidation.message);
            }
        }

        
        private void listBox_Priorities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int itemsSelected = listBox_Priorities.SelectedItems.Count;
            button_Edit.Visibility = itemsSelected == 1 ? Visibility.Visible : Visibility.Collapsed;
            button_Remove.Visibility = itemsSelected > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            currentOperation = CurrentOperationsEnum.Remove;
            List<TicketPriority> entriesToRemove = listBox_Priorities.SelectedItems.OfType<TicketPriority>().ToList();

            if (entriesToRemove.Any()) 
            {
                string prompt = $"{listBox_Priorities.SelectedItems.Count} " + $"entriess are going to be deleted. Are you sure?";
                if (MessageBox.Show(prompt, "Delete operation", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                    == MessageBoxResult.OK)
                {
                    int removed = Presenter.Delete(entriesToRemove.ToArray());
                    MessageBox.Show($"{removed} enries removed");
                    RefreshWindow();
                }
            }
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            currentOperation = CurrentOperationsEnum.Edit;
            textBox_UserInput.Visibility = Visibility.Visible;
            button_Add.Visibility = Visibility.Collapsed;
            button_Edit.Visibility = Visibility.Collapsed;
            button_Remove.Visibility= Visibility.Collapsed;
            if (listBox_Priorities.SelectedItem is TicketPriority selectedItem) 
            {
                currentPriority = selectedItem;
                textBox_UserInput.Text = currentPriority.Name;
            }
            button_Save.Visibility = Visibility.Collapsed;
            button_Cancel.Visibility = Visibility.Visible;
        }

        private void TryToUpdate() 
        {
            TicketPriority entryToUpdate = new TicketPriority(){Id = currentPriority.Id, Name = textBox_UserInput.Text};
            var updated = Presenter.UpdateDB(entryToUpdate);
            if (!updated.result) 
            {
                string msg = $"Entry [{currentPriority?.Name}] update failed:\n{updated.message}";
                MessageBox.Show(msg);
                errorDetection.Invoke(textBox_UserInput, true, msg);
            } 
            else 
            {
                MessageBox.Show($"Entry [{currentPriority.Name}] successfully changed by [{entryToUpdate.Name}]");
            }
        }
    }
}
