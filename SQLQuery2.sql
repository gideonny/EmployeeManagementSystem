
CREATE TABLE Employee (
    EmployeeID int IDENTITY(1, 1) NOT NULL,
    EmployeeName nvarchar(50) NOT NULL,
    Position nvarchar(50) NOT NULL,
    HourlyPayRate decimal(10,2) NULL,
    CONSTRAINT "PK_Employee" PRIMARY KEY CLUSTERED ("EmployeeID")
);