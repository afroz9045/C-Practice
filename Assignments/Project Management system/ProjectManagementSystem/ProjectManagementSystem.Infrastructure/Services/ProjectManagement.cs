using Pms.Core.Entities;
using ProjectManagementSystem.Core.Contracts;
using ProjectManagementSystem.Core.Models;
using System.Collections;
using static ProjectManagementSystem.Infrastructure.Data.ProjectManagementDataInMemory;

namespace ProjectManagementSystem.Infrastructure.Services
{
    public class ProjectManagement : IProjectManagement
    {

        // Assignment
        public List<Assignment> GetAssignments()
        {
            return assignments;
        }

        // Get Department
        public IEnumerable<Department>? GetDepartment(int? deptId = null, string? deptName = null)
        {
            try
            {
                if (deptId != null || deptName != null)
                {
                   
                        if ((deptName != "Marketing".ToLower() && deptName != "Finance".ToLower() && deptName != "Accounting".ToLower())&& (deptId != 1 && deptId != 2 && deptId != 3))
                        {
                            throw new InvalidDataException();
                        }
                    var findDept = from department in department
                                   where (deptId == null || department.DeptId == deptId)
                                   && (deptName == null || department.DeptName.ToLower() == deptName)
                                   select department;
                    return findDept;

                }
                return department;
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Please enter valid input");
                return null;
            }



        }
        // Get Project
        public IEnumerable<Project>? GetProject(int? departmentId = null, string? projectName = null, string? departmentName = null)
        {
            try
            {
                if (departmentId != null || projectName != null || departmentName != null)
                {
                    if ((departmentName.Equals("Marketing", StringComparison.InvariantCultureIgnoreCase)&& departmentName.Equals("Finance", StringComparison.CurrentCultureIgnoreCase) && departmentName.Equals("Accounting",StringComparison.CurrentCultureIgnoreCase) && (departmentId != 1 && departmentId != 2 && departmentId != 3)))
                        {
                        throw new InvalidDataException();
                    }
                    var findProject = from project in projects
                                      join depart in department
                                      on project.DepartmentId equals depart.DeptId
                                      where (departmentId == null || project.DepartmentId == departmentId)
                                      && (projectName == null || project.ProjectName == projectName.ToLower())
                                      && (departmentName == null || depart.DeptName.ToLower() == departmentName.ToLower())
                                      select project;
                    return findProject;
                }
                return projects;
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Please enter valid input");
                return null;
            }


        }

        // Get Employee
        public IEnumerable<Employee> GetEmployees(int? deptId = null, int? empId = null)
        {
            if (deptId != null || empId != null)
            {
                var findEmployees = from emp in employees
                                    where (deptId == null || emp.DepartmentId == deptId)
                                    && (empId == null || emp.EmployeeNumber == empId)
                                    select emp;
                return findEmployees;
            }
            return employees;
        }

        // Project and Assignment details
        public IEnumerable<ProjectResourceDetails>? GetProjectAndAssignmentDetails(string? deptName = null, int? departmentId = null)
        {
            try
            {
                if (departmentId != null || deptName != null)
                {
                    if (!(deptName.Equals("Finance",StringComparison.InvariantCultureIgnoreCase)) 
                        && !(deptName.Equals("Marketing", StringComparison.InvariantCultureIgnoreCase)) 
                        && !(deptName.Equals("Accounting", StringComparison.InvariantCultureIgnoreCase)) 
                        && (!(departmentId == 1) && !(departmentId == 2) && !(departmentId == 3)))
                    {
                        throw new InvalidDataException();
                    }
                    
                }
                var projectAndAssignment = (from dept in department
                                            join emp in employees
                                            on dept.DeptId equals emp.DepartmentId
                                            join proj in projects
                                            on emp.DepartmentId equals proj.DepartmentId
                                            join assign in assignments
                                            on emp.EmployeeNumber equals assign.EmployeeNumber
                                            where (dept.DeptId == departmentId)||(dept.DeptName.Equals(deptName,StringComparison.InvariantCultureIgnoreCase))
                                            select new
                                            {
                                                departmentName = dept.DeptName,
                                                projectName = proj.ProjectName,
                                                assignmentName = assign.AssignmentName,
                                                employeeName = emp.EmployeeName
                                            }).Distinct();
                var combinedData = from data in projectAndAssignment
                                   select new ProjectResourceDetails()
                                   {
                                       departmentName = data.departmentName,
                                       projectName = data.projectName,
                                       assignmentName = data.assignmentName,
                                       employeeName = data.employeeName
                                   };

                return combinedData;

            }
            catch (InvalidDataException)
            {

                Console.WriteLine("Please Enter Valid Department name!");
                return null;
            }


        }

        // Searching
        bool isSearchFound = false;
        public void SearchEntity(string searchKeyword)
        {

            var combinedData = GetProjectAndAssignmentDetails();
            foreach (var data in combinedData)
            {

                if (data.departmentName.ToLower().Contains(searchKeyword) || data.projectName.ToLower().Contains(searchKeyword) || data.assignmentName.ToLower().Contains(searchKeyword) || data.employeeName.ToLower().Contains(searchKeyword))
                {
                    Console.WriteLine($"Results found: {data.departmentName}\t{data.projectName}\t{data.assignmentName}\t{data.employeeName}");
                    isSearchFound = true;
                }


            }
            if (!isSearchFound)
            {
                Console.WriteLine($"Sorry {searchKeyword} is not found!");

            }


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
        public IEnumerable<TotalSalaryByDepartment> GetTotalSalaryByEachDepartment()
        {
            var totalSalaryPerDept = from emp in employees
                                     group emp by emp.DepartmentId into dept
                                     select new TotalSalaryByDepartment()
                                     {
                                         departmentId = dept.Key,
                                         totalSalary = dept.Sum(s => s.Salary)
                                     };
            return totalSalaryPerDept;
        }
        public static void GetDetails<T>(IEnumerable<T> collectiondata)
        {
            foreach (var data in collectiondata)
            {
                Console.WriteLine(data?.ToString() + "\n\n");
            }

        }

        public static void GetSpecificDetails(IEnumerable<ProjectResourceDetails>? collection = null, IEnumerable<TotalSalaryByDepartment>? totalSalary = null)
        {
            if (collection != null)
            {
                foreach (var data in collection)
                {

                    Console.WriteLine($"\t{data.departmentName}\t{data.projectName}\t{data.assignmentName}\t{data.employeeName}");
                }
            }
            else if (totalSalary != null)
            {
                foreach (var data in totalSalary)
                {
                    Console.WriteLine($"\t{data.departmentId}\t\t{data.totalSalary}");
                }
            }
            else
            {
                return;
            }
        }
    }
}

