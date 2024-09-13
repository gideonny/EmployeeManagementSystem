using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace AssignmentPracticalGN8883421
{
    public class TextFileManagement
    {
        public static Employee LoadEmployeeFromFile(string fileName)
        {
            Employee employee = new Employee();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string employeeDataFromFile;
                while ((employeeDataFromFile = reader.ReadLine()) != null)
                {
                    //split the line on the '=' to separate the key and the value
                    string[] parts = employeeDataFromFile.Split('=');
                    if (parts.Length == 2)
                    {
                        string key = parts[0];
                        string value = parts[1];

                        switch (key)
                        {
                            case "EmployeeID":
                                employee.EmployeeID = int.Parse(value);
                                break;
                            case "EmployeeName":
                                employee.EmployeeName = value;
                                break;
                            case "Position":
                                employee.Position = value;
                                break;
                            case "HourlyPayRate":
                                employee.HourlyPayRate = decimal.Parse(value);
                                break;
                        }
                    }
                }
            }
            return employee;
        }
    }
}
