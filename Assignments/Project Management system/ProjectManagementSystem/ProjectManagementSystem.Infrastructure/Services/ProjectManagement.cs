using Pms.Core.Entities;
using ProjectManagementSystem.Core.Contracts;
using System.Text.Json;
using static ProjectManagementSystem.Infrastructure.Data.ProjectManagementDataInMemory;
using System.Text;

using System.Collections;

namespace ProjectManagementSystem.Infrastructure.Services
{
    public class ProjectManagement : IProjectManagement
    {
        // Department
        public List<Department> GetDepartments()
        {
            return department;
        }
        // Employee
        public List<Employee> GetEmployees()
        {
            return employees;
        }
        // Project
        public List<Project> GetProjects()
        {
            return projects;
        }
        // Assignment
        public List<Assignment> GetAssignments()
        {
            return assignments;
        }

        // Get Department by Id
        public IEnumerable<Department> GetDepartments(int deptId)
        {
            var findDeptById = from department in department
                               where department.DeptId == deptId
                               select department;
            return findDeptById;

        }

        // Get Department by Department Name
        public IEnumerable<Department> GetDepartments(string deptName)
        {
            var findDeptByName = from department in department
                                 where department.DeptName == deptName
                                 select department;
            return findDeptByName;
        }


        // Get Project details by department Id
        public List<Project> GetProjects(int departmentId)
        {
            var findProjectById = from project in projects
                                  where project.DepartmentId == departmentId
                                  select project;
            return findProjectById.ToList();
        }

        // Get Project details by Project Name
        public List<Project> GetProjects(string projectName)
        {
            var findProjectByName = from project in projects
                                    where project.ProjectName == projectName
                                    select project;
            return findProjectByName.ToList();

        }
        // Getting Projects by Dept Name
        public List<Project> GetProjectsByDepartmentName(string departmentName)
        {
            var projectFilteredData = from project in projects
                                      join depart in department
                                      on project.DepartmentId equals depart.DeptId
                                      where depart.DeptName == departmentName
                                      select project;
            return projectFilteredData.ToList();
            
        }

        // Getting Employees By Dept Id
        public IEnumerable<Employee> GetEmployeesByDeptId(int deptId)
        {
            var employeeFilteredData = from employee in employees
                                       where employee.DepartmentId == deptId
                                       select employee;
            return employeeFilteredData;
        }

        // Getting Employees details Employee Id
        public IEnumerable<Employee> GetEmployeeDetailsByEmpId(int empId)
        {
            var empRecord = employees.Where(employee => employee.EmployeeNumber == empId).ToList();
            return empRecord;
        }
        
        // Getting Number of Employees Working in each department
        public IEnumerable GetNumberOfEmployeesInEachDepartment()
        {
            var numberOfEmployees = from emp in employees
                                    group emp by emp.DepartmentId into groupedDept
                                    select new
                                    {
                                        departmentId = groupedDept.Key,
                                        count = groupedDept.Count()
                                    };
            foreach (var count in numberOfEmployees)
            {
                Console.WriteLine($"\t{count.departmentId}\t\t{count.count}");
            }

            return numberOfEmployees;

        }

        // Getting Total Salary Paid for each department
        public IEnumerable GetTotalSalaryByEachDepartment()
        {
            var totalSalaryPerDept = from emp in employees
                                     group emp by emp.DepartmentId into dept
                                     select new
                                     {
                                         departmentId = dept.Key,
                                         totalSalary = dept.Sum(s => s.Salary)
                                     };
            foreach (var departmentSalary in totalSalaryPerDept)
            {
                Console.WriteLine($"\t{departmentSalary.departmentId}\t\t{departmentSalary.totalSalary}");
            }
            return totalSalaryPerDept;
        }
        public static void GetDetails<T>(IEnumerable<T> collectiondata)
        {
            foreach (var data in collectiondata)
            {
                Console.WriteLine(data?.ToString());
            }

        }

        public static void GetSpecificDetails(IEnumerable collection)
        {
            foreach (var data in collection)
            {

                Console.WriteLine($"{data}");
            }
        }
    }
}
