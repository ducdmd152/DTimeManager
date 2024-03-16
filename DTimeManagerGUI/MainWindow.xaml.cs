using DTimeManagerBO;
using DTimeManagerService;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;

namespace DTimeManagerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCountdownTimer();
            InitAlarmEventManagement();
        }

        // COUNTDOWN TIMER TAB
        private CountdownTimerManagerService countdownTimerManager;
        private void InitializeCountdownTimer()
        {            
            countdownTimerManager = new CountdownTimerManagerService(TimeSpan.Zero);
            countdownTimerManager.TimerTick += TimerTickHandler;
            countdownTimerManager.TimerElapsed += TimerElapsedHandler;
            CountdownTimerReset();
        }

        private void CountdownTimerReset()
        {
            tbHours.Text = "00";
            tbMins.Text = "00";
            tbSeconds.Text = "00";
            btnAction.Content = "START";
            countdownTimerManager.Reset();
        }

        private void TimerTickHandler(object sender, string remainingTime)
        {
            string[] parts = remainingTime.Split(':');
            if (parts.Length == 3)
            {
                Dispatcher.Invoke(() =>
                {
                    tbHours.Text = parts[0];
                    tbMins.Text = parts[1];
                    tbSeconds.Text = parts[2];
                });
            }
        }

        private void TimerElapsedHandler(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                CountdownTimerReset();
                MessageBox.Show("Time's uppppppppppppp!", "HEY HEY HEY!!!!", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            int hours = int.Parse(tbHours.Text);
            int mins = int.Parse(tbMins.Text);
            int seconds = int.Parse(tbSeconds.Text);

            if (hours == 0 && mins == 0 && seconds == 0) {
                MessageBox.Show("Please enter the time to count!", "Hey hey!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (hours > 1000)
            {
                MessageBox.Show("The hours is too large!", "Invalid Input!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                
            if (mins < 0 || mins > 59)
            {
                MessageBox.Show("Please enter the minutes between 0 and 59!", "Invalid Input!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (seconds < 0 || seconds > 59)
            {
                MessageBox.Show("Please enter the seconds between 0 and 59!", "Invalid Input!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TimeSpan time = new TimeSpan(hours, mins, seconds);

            if (!countdownTimerManager.IsRunning)
            {
                countdownTimerManager.Start(time);
                btnAction.Content = "PAUSE";
            }
            else
            {
                if (!countdownTimerManager.IsPaused)
                {
                    countdownTimerManager.Pause();
                    btnAction.Content = "RESUME";
                }
                else
                {
                    countdownTimerManager.Resume();
                    btnAction.Content = "PAUSE";
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            CountdownTimerReset();
        }
        // ALARM EVENT MANAGEMENT TAB
        private OperatorMode mode;
        private IAlarmEventService alarmEventService;
        private List<AlarmEvent> alarmEventList;
        private void InitAlarmEventManagement()
        {
            alarmEventService = new AlarmEventService();
            UpdateOperatorMode(OperatorMode.Add);
            LoadData();         
        }

        private void UpdateOperatorMode(OperatorMode mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case OperatorMode.Add:
                    btnSave.Content = "NEW";
                    dgAlarms.SelectedIndex = -1;
                    btnRemove.Content = "CANCEL";
                    ResetInfoToEmpty();
                    //lbMode.Content = "Adding Mode";
                    //lbMode.Background = new SolidColorBrush(Colors.LightGreen);
                    break;
                case OperatorMode.Update:
                    btnSave.Content = "UPDATE";
                    //lbMode.Background = new SolidColorBrush(Colors.LightBlue);
                    //lbMode.Content = "Updating Mode";
                    break;
            }
        }

        private void LoadData()
        {
            try
            {
                //if (dgAlarms.Items.Count == 0)
                //    UpdateOperatorMode(OperatorMode.Add);

                //int index = dgAlarms.SelectedIndex;
                alarmEventList = alarmEventService.GetAll();
                dgAlarms.ItemsSource = alarmEventList;
                //dgAlarms.SelectedIndex = mode == OperatorMode.Add ? -1 : Math.Min(index, dgAlarms.Items.Count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not load data: " + ex.Message, "Something is wrong!");
            }
        }

        private void ResetInfoToEmpty()
        {
            tbEventName.Text = string.Empty;
            tbAlarmHours.Text = "12";
            tbAlarmMins.Text = "00";
            ckbActive.IsChecked = true;
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Id" || e.PropertyName == "Manager")
            {
                e.Cancel = true;
            }
            else
            {
                if (e.Column is DataGridTextColumn textColumn)
                {
                    textColumn.Width = new DataGridLength(300);
                }
            }
        }

        private void dgAlarms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgAlarms.SelectedIndex >= 0 && dgAlarms.SelectedIndex < dgAlarms.Items.Count)
            {
                UpdateOperatorMode(OperatorMode.Update);
                AlarmEvent selectedItem = (AlarmEvent)dgAlarms.SelectedItem;
                tbEventName.Text = selectedItem.Name;
                tbAlarmHours.Text = selectedItem.Time.Hours.ToString();
                tbAlarmMins.Text = selectedItem.Time.Minutes.ToString();
                ckbActive.IsChecked = selectedItem.IsActived;
            }
        }
        // Window Events
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMimimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {              
                string newText = textBox.Text + e.Text;
                int result;
                e.Handled = !(int.TryParse(newText, out result) && newText.Length <= 2);
            }
        }

        private bool IsTextNumeric(string text)
        {
            return !string.IsNullOrEmpty(text) && text.All(char.IsDigit);
        }
        private enum OperatorMode
        {
            Add,
            Update
        }
    }
}
