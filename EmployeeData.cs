using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using System.Data.Common;

namespace AssignmentPracticalGN8883421
{
    public class EmployeeData
    {
        public static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Personnel;Integrated Security=True";
        private SqlConnection _connection;
        private SqlDataAdapter _adapter;
        private SqlCommandBuilder _commandBuilder;
        private DataSet _dataSet;
        private DataTable _dataTable;

        //property
        public static string ConnectionStr { get { return connStr; } }

        //constructor
        public EmployeeData()
        {
            string query = "Select EmployeeID, EmployeeName, Position, HourlyPayRate from Employee;";
            _connection = new SqlConnection(ConnectionStr);
            _adapter = new SqlDataAdapter(query, _connection);
            _commandBuilder = new SqlCommandBuilder(_adapter);

            FillDataSet();
        }

        //Methods
        public DataTable GetAllEmployees()
        {
            SqlConnection conn = new SqlConnection(ConnectionStr);
            string query = "Select EmployeeID, EmployeeName, Position, HourlyPayRate from Employee;";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

            // Create DataSet
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Employee");

            DataTable tblEmployee = ds.Tables["Employee"];
            return tblEmployee;
        }

        public void FillDataSet()
        {
            _dataSet = new DataSet();
            _adapter.Fill(_dataSet);
            _dataTable = _dataSet.Tables[0];

            // Define Primary Key - Auto Increment
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _dataTable.Columns["EmployeeID"];
            pk[0].AutoIncrement = true;
            _dataTable.PrimaryKey = pk;
        }

        public void InsertEmployee(int employeeID, string employeeName, string position, decimal hourlyPayRate)
        {
            DataRow newRow = _dataTable.NewRow();

            // Assign Values from Parameters
            newRow["EmployeeName"] = employeeName;
            newRow["Position"] = position;
            newRow["HourlyPayRate"] = hourlyPayRate;

            _dataTable.Rows.Add(newRow);

            _adapter.InsertCommand = _commandBuilder.GetInsertCommand();
            _adapter.Update(_dataTable);
        }
    }
}
