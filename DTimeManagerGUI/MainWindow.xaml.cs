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
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //if (e.PropertyName == "Password" || e.PropertyName == "RoleId")
            //{
            //    e.Cancel = true;
            //}
        }

        private void dgAlarms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (dgUsers.SelectedIndex >= 0 && dgUsers.SelectedIndex < dgUsers.Items.Count)
            //{
            //    UpdateOperatorMode(OperatorMode.Update);
            //    User selectedItem = (User)dgUsers.SelectedItem;
            //    txtEmail.Text = selectedItem.Email;
            //    txtName.Text = selectedItem.Name;
            //    txtPassword.Password = selectedItem.Password;
            //    txtPhone.Text = selectedItem.Phone;
            //    txtAddress.Text = selectedItem.Address;
            //    chkStatus.IsChecked = selectedItem.Enabled;
            //    cmbUserRole.SelectedValue = selectedItem.RoleId;
            //}
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
    }
}
