using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Data.SqlClient;


namespace AssignmentPracticalGN8883421
{   
    public partial class EditEmployeeWindow : Window
    {
        private DataTable _employeeTable;
        public EditEmployeeWindow(DataTable employeeTable, SqlDataAdapter adapter, SqlCommandBuilder commandBuilder)
        {
            InitializeComponent();
            _employeeTable = employeeTable;
            _adapter = adapter;
            _commandBuilder = commandBuilder;
            _dataTable = employeeTable;
            //Bind DataTable to DataGrid
            EmployeesDataGrid.ItemsSource = _employeeTable.DefaultView;

            this.Closed += EditEmployeeWindow_Closed;

        }

        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _commandBuilder;
        private DataTable _dataTable;

        private void btnEditEmployeesData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int employeeID = int.Parse(txtEmployeeID.Text);
                string employeeName = txtEmployeeName.Text;
                string position = txtPosition.Text;
                decimal hourlyPayRate = decimal.Parse(txtHourlyPayRate.Text);

                DataRowView selectedRow = EmployeesDataGrid.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    selectedRow["EmployeeID"] = employeeID;
                    selectedRow["EmployeeName"] = employeeName;
                    selectedRow["Position"] = position;
                    selectedRow["HourlyPayRate"] = hourlyPayRate;

                    _adapter.UpdateCommand = _commandBuilder.GetUpdateCommand();
                    _adapter.Update(_dataTable);

                    MessageBox.Show("Employee record updated successfully.");
                }
                else
                {
                    MessageBox.Show("No employee record selected.");
                }
            }
            
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid data in the fields.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        

        private void RefreshEmployeeDataGrid()
        {
            EmployeesDataGrid.ItemsSource = null;
            EmployeesDataGrid.ItemsSource = _employeeTable.DefaultView;
        }

        private void ClearFields()
        {
            txtEmployeeID.Text = "";
            txtEmployeeName.Text = "";
            txtPosition.Text = "";
            txtHourlyPayRate.Text = "";
        }

        private void EmployeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesDataGrid.SelectedItem is DataRowView selectedRow)
            {
                txtEmployeeID.Text = selectedRow["EmployeeID"].ToString();
                txtEmployeeName.Text = selectedRow["EmployeeName"].ToString();
                txtPosition.Text = selectedRow["Position"].ToString();
                txtHourlyPayRate.Text = selectedRow["HourlyPayRate"].ToString();
            }
        }

        private void EditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            // Logic for updating main window
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.RefreshEmployeeData();
            }
        }
    }
}
