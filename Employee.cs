using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentPracticalGN8883421
{
    public class Employee
    {
        //properties
        //1a)
        public int EmployeeID { get; set; }

        //1b)
        public string EmployeeName { get; set; }

        //1c)
        public string Position { get; set; }

        //1d)
        public decimal HourlyPayRate { get; set; }

        //constructors
        //2a)
        public Employee(int employeeID, string employeeName, string position, decimal hourlyPayRate)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            Position = position;
            HourlyPayRate = hourlyPayRate;
        }

        //2b)
        public Employee(int employeeID, string employeeName, decimal hourlyPayRate)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            Position = "";
            HourlyPayRate = hourlyPayRate;
        }

        //2c)
        public Employee()
        {
            EmployeeID = 0;
            EmployeeName = "";
            Position = "";
            HourlyPayRate = 0;
        }
    }
}
