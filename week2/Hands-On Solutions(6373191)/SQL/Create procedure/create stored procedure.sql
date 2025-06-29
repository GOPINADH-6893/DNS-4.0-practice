-- Drop if tables already exist
IF OBJECT_ID('Employees', 'U') IS NOT NULL DROP TABLE Employees;
IF OBJECT_ID('Departments', 'U') IS NOT NULL DROP TABLE Departments;
GO

-- Create Departments table
CREATE TABLE Departments ( 
    DepartmentID INT PRIMARY KEY, 
    DepartmentName VARCHAR(100) 
);
GO

-- Create Employees table
IF OBJECT_ID('Employees', 'U') IS NOT NULL DROP TABLE Employees;
GO

CREATE TABLE Employees ( 
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),  -- Auto-generate ID
    FirstName VARCHAR(50), 
    LastName VARCHAR(50), 
    DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID), 
    Salary DECIMAL(10,2), 
    JoinDate DATE 
);
GO


-- Insert Departments
INSERT INTO Departments (DepartmentID, DepartmentName) VALUES 
(1, 'HR'), 
(2, 'Finance'), 
(3, 'IT'), 
(4, 'Marketing');
GO

-- Insert Employees
INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES 
(1, 'John', 'Doe', 1, 5000.00, '2020-01-15'), 
(2, 'Jane', 'Smith', 2, 6000.00, '2019-03-22'), 
(3, 'Michael', 'Johnson', 3, 7000.00, '2018-07-30'), 
(4, 'Emily', 'Davis', 4, 5500.00, '2021-11-05');
GO

-- Drop existing procedures if they exist
IF OBJECT_ID('sp_GetEmployeesByDepartment', 'P') IS NOT NULL DROP PROCEDURE sp_GetEmployeesByDepartment;
IF OBJECT_ID('sp_InsertEmployee', 'P') IS NOT NULL DROP PROCEDURE sp_InsertEmployee;
GO

-- Create procedure to get employees by department
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO

-- Create procedure to insert new employee
CREATE PROCEDURE sp_InsertEmployee 
    @FirstName VARCHAR(50),  
    @LastName VARCHAR(50),  
    @DepartmentID INT,  
    @Salary DECIMAL(10,2),  
    @JoinDate DATE
AS
BEGIN
    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate) 
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);
END;
GO

-- To insert a new employee
EXEC sp_InsertEmployee 
    @FirstName = 'Sara',
    @LastName = 'Lee',
    @DepartmentID = 2,
    @Salary = 6200.00,
    @JoinDate = '2023-01-10';

-- To fetch employees from Finance department (DepartmentID = 2)
EXEC sp_GetEmployeesByDepartment @DepartmentID = 2;
