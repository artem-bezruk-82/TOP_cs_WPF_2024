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

namespace BugTracker.View
{
    /// <summary>
    /// Interaction logic for Window_Types.xaml
    /// </summary>
    public partial class Window_Types : Window
    {
        private delegate void ErrorDetection(object sender, bool errorDetected, string errorMessage);
        private event ErrorDetection errorDetection;

        private enum CurrentOperationsEnum
        {
            None,
            Add,
            Edit,
            Remove
        }

        private CurrentOperationsEnum currentOperation;
        private TicketType currentType;
        private ObservableCollection<TicketType> ticketTypes;

        public string? watermarkText
        {
            get
            {
                return currentOperation switch
                {
                    CurrentOperationsEnum.Add => "Please enter priority name",
                    CurrentOperationsEnum.Edit => currentType?.Name,
                    _ => string.Empty,
                };
            }
        }

        public Window_Types()
        {
            currentType = new TicketType();
            ticketTypes = new ObservableCollection<TicketType>();
            currentOperation = CurrentOperationsEnum.None;
            InitializeComponent();
            LoadDataFromDB();
            ResetControls();
            errorDetection += Window_errorDetection;
        }

        private void InitializeMembers()
        {
            currentOperation = CurrentOperationsEnum.None;
            currentType = new TicketType();
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

            listBox.ItemsSource = ticketTypes;
        }

        private void LoadDataFromDB()
        {
            ticketTypes = new ObservableCollection<TicketType>(Presenter.GetTicketTypes());
        }

        private void RefreshWindow()
        {
            InitializeMembers();
            LoadDataFromDB();
            ResetControls();
        }

        private void Window_errorDetection(object sender, bool errorDetected, string errorMessage)
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
            currentType = new TicketType();
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
            if (ticketTypes.Any(p => p.Name.ToLower() == textBox_UserInput.Text.ToLower()))
            {
                msg = "Entry already exists";
                return (false, msg);
            }
            return (true, msg);
        }

        private void TryToAdd()
        {
            var added = Presenter.AddToDB(currentType);
            if (!added.result)
            {
                string msg = $"Entry [{currentType?.Name}] adding failed:\n{added.message}";
                MessageBox.Show(msg);
                errorDetection.Invoke(textBox_UserInput, true, msg);
            }
            else
            {
                MessageBox.Show($"Entry [{currentType?.Name}] added successfully ");
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
                        currentType.Name = textBox_UserInput.Text;
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


        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int itemsSelected = listBox.SelectedItems.Count;
            button_Edit.Visibility = itemsSelected == 1 ? Visibility.Visible : Visibility.Collapsed;
            button_Remove.Visibility = itemsSelected > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            currentOperation = CurrentOperationsEnum.Remove;
            List<TicketType> entriesToRemove = listBox.SelectedItems.OfType<TicketType>().ToList();

            if (entriesToRemove.Any())
            {
                string prompt = $"{listBox.SelectedItems.Count} " + $"entriess are going to be deleted. Are you sure?";
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
            button_Remove.Visibility = Visibility.Collapsed;
            if (listBox.SelectedItem is TicketType selectedItem)
            {
                currentType = selectedItem;
                textBox_UserInput.Text = currentType.Name;
            }
            button_Save.Visibility = Visibility.Collapsed;
            button_Cancel.Visibility = Visibility.Visible;
        }

        private void TryToUpdate()
        {
            TicketType entryToUpdate = new TicketType() { Id = currentType.Id, Name = textBox_UserInput.Text };
            var updated = Presenter.UpdateDB(entryToUpdate);
            if (!updated.result)
            {
                string msg = $"Entry [{currentType?.Name}] update failed:\n{updated.message}";
                MessageBox.Show(msg);
                errorDetection.Invoke(textBox_UserInput, true, msg);
            }
            else
            {
                MessageBox.Show($"Entry [{currentType.Name}] successfully changed by [{entryToUpdate.Name}]");
            }
        }
    }
}
