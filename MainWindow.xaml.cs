using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace AssignmentPracticalGN8883421
{
    /// Practical
    /// Gideon Nyamuame
    /// Instructor: Sukhbir Tatla <summary>

  
    
    public partial class MainWindow : Window
    {
        private EmployeeData _employeeData;

        public MainWindow()
        {
            InitializeComponent();
            _employeeData = new EmployeeData();
            grdEmployeeTable.ItemsSource = _employeeData.GetAllEmployees().DefaultView;
        }

       

        private void LoadAndInsertEmployees()
        {
            //path to employee folder
            string folderPath = @"..\..\..\Employees";
            //path for every "employee" file. grabs all .txt files in the folder
            string[] filePaths = Directory.GetFiles(folderPath, "*.txt");

            foreach (string filePath in filePaths)
            {
                //loading employee from file
                Employee employee = TextFileManagement.LoadEmployeeFromFile(filePath);

                //inserting employees into the database
                _employeeData.InsertEmployee(
                    employee.EmployeeID,
                    employee.EmployeeName,
                    employee.Position,
                    employee.HourlyPayRate
                    );
            }
            //refresh Datagrid
            grdEmployeeTable.ItemsSource = _employeeData.GetAllEmployees().DefaultView;
        }

        private DataTable SearchEmployeesByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(EmployeeData.ConnectionStr))
            {
                // Using LIKE operator to support partial matches
                string query = "SELECT EmployeeID, EmployeeName, Position, HourlyPayRate FROM Employee WHERE EmployeeName LIKE @EmployeeName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", "%" + name + "%"); // % for wildcard search
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable results = new DataTable();
                        adapter.Fill(results);
                        return results;
                    }
                }
            }
        }



        public static SqlDataAdapter GetAdapter()
        {
            string query = "Select EmployeeID, EmployeeName, Position, HourlyPayRate from Employee;";
            SqlConnection conn = new SqlConnection(EmployeeData.ConnectionStr);
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            return adapter;
        }

        public void RefreshEmployeeData()
        {
            // Reload the data from the database and refresh the data grid
            grdEmployeeTable.ItemsSource = _employeeData.GetAllEmployees().DefaultView;
        }

        private void btnLoadAllEmployees_Click(object sender, RoutedEventArgs e)
        {
            LoadAndInsertEmployees();
        }

        private void btnEditEmployees_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the employee data
            DataTable employeeTable = _employeeData.GetAllEmployees();

            //method so i can get sqladapter in my editemployees window
            SqlDataAdapter adapter = GetAdapter();
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

            // Create and show the EditEmployeeWindow, passing the employee data
            EditEmployeeWindow editWindow = new EditEmployeeWindow(employeeTable, adapter, commandBuilder);
            editWindow.Show();
        }

        

        private void btnShowAllEmployees_Click(object sender, RoutedEventArgs e)
        {
            DataTable allEmployees = _employeeData.GetAllEmployees();
            grdEmployeeTable.ItemsSource = allEmployees.DefaultView;
        }

        private void btnSearchEmployees_Click(object sender, RoutedEventArgs e)
        {
            {
                string searchName = txtSearchName.Text.Trim();
                if (!string.IsNullOrEmpty(searchName))
                {
                    // Call the method to search employees by name
                    DataTable searchResults = SearchEmployeesByName(searchName);
                    grdEmployeeTable.ItemsSource = searchResults.DefaultView;
                }
                else
                {
                    MessageBox.Show("Please enter a name to search.");
                }
            }
        }
    }  
}