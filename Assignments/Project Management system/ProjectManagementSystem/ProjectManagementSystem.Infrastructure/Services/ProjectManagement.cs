using Pms.Core.Entities;
using ProjectManagementSystem.Core.Models;
using System.Collections;
using static ProjectManagementSystem.Infrastructure.Data.ProjectManagementDataInMemory;

namespace ProjectManagementSystem.Infrastructure.Services
{
    public class ProjectManagement : IProjectManagement
    {

        /// <summary>
        /// Use this method to get assignment details
        /// </summary>
        /// <returns>It returns Assignment details</returns>
        public List<Assignment> GetAssignments()
        {
            return assignments;
        }

        /// <summary>
        ///  Use this method to get Department details 
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="deptName"></param>
        /// <returns>It returns Department details</returns>
        public IEnumerable<Department>? GetDepartment(int? deptId = null, string? deptName = null)
        {
            if (deptId != null || deptName != null)
            {
                var findDept = from department in department
                               where (deptId == null || department.DeptId == deptId)
                               && (deptName == null || department.DeptName.ToLower() == deptName)
                               select department;
                return findDept;

            }
            return department;
        }
        /// <summary>
        /// Use this method to get project details
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="projectName"></param>
        /// <param name="departmentName"></param>
        /// <returns>It returns project details</returns>
        public IEnumerable<Project>? GetProject(int? departmentId = null, string? projectName = null, string? departmentName = null)
        {
            if (departmentId != null || projectName != null || departmentName != null)
            {
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

        /// <summary>
        ///  Use this method to get employee details
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="empId"></param>
        /// <returns>It returns employee details</returns>
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

        /// <summary>
        /// Use this method to get project and assignment details
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="departmentId"></param>
        /// <returns>It returns project resource details</returns>
        public IEnumerable<ProjectResourceDetails>? GetProjectAndAssignmentDetails(string? deptName = null, int? departmentId = null)
        {

            var projectAndAssignment = (from dept in department
                                        join emp in employees
                                        on dept.DeptId equals emp.DepartmentId
                                        join proj in projects
                                        on emp.DepartmentId equals proj.DepartmentId
                                        join assign in assignments
                                        on emp.EmployeeNumber equals assign.EmployeeNumber
                                        where (departmentId == null || dept.DeptId == departmentId)
                                        && (deptName == null || dept.DeptName.Equals(deptName, StringComparison.InvariantCultureIgnoreCase))
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
                                   DepartmentName = data.departmentName,
                                   ProjectName = data.projectName,
                                   AssignmentName = data.assignmentName,
                                   EmployeeName = data.employeeName
                               };

            return combinedData;
        }


        // Searching
        bool isSearchFound = false;
        public IEnumerable<ProjectResourceDetails>  SearchEntity(string searchKeyword)
        {
            var searchData = from data in GetProjectAndAssignmentDetails()
                             where data.DepartmentName.ToLower().Contains(searchKeyword) || data.EmployeeName.ToLower().Contains(searchKeyword) || data.ProjectName.ToLower().Contains(searchKeyword) || data.AssignmentName.ToLower().Contains(searchKeyword)
                             select data;
            return searchData;
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
                                         DepartmentId = dept.Key,
                                         TotalSalary = dept.Sum(s => s.Salary)
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

                    Console.WriteLine($"\t{data.DepartmentName}\t\t{data.ProjectName}\t{data.AssignmentName}\t\t\t{data.EmployeeName}");
                }
            }
            else if (totalSalary != null)
            {
                foreach (var data in totalSalary)
                {
                    Console.WriteLine($"\t{data.DepartmentId}\t\t{data.TotalSalary}");
                }
            }
            else
            {
                return;
            }
        }
    }
}

