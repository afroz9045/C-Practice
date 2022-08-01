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
        public IEnumerable<Department> GetDepartment(int? deptId = null, string? deptName = null)
        {
            if (deptId != null || deptName != null)
            {
                var findDept = from department in department
                               where (deptId == null || department.DeptId == deptId)
                               && (deptName == null || department.DeptName == deptName)
                               select department;
                return findDept;

            }
            return department;


        }

        // Get Project
        public IEnumerable<Project> GetProject(int? departmentId = null, string? projectName = null, string? departmentName = null)
        {
            if (departmentId != null || projectName != null || departmentName != null)
            {
                var findProject = from project in projects
                                  join depart in department
                                  on project.DepartmentId equals depart.DeptId
                                  where (departmentId == null || project.DepartmentId == departmentId)
                                  && (projectName == null || project.ProjectName == projectName)
                                  && (departmentName == null || depart.DeptName == departmentName)
                                  select project;
                return findProject;
            }
            return projects;

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


        // Get Assignment
        //public IEnumerable<Assignment> GetAssignment(int? deptId = null, string? deptName = null)

        // Project and Assignment details
        public IEnumerable<CombinedEntities> GetProjectAndAssignmentDetails(int? departmentId=null)
        {
            var projectAndAssignment = (from dept in department
                                        join emp in employees
                                        on dept.DeptId equals emp.DepartmentId
                                        join proj in projects
                                        on emp.DepartmentId equals proj.DepartmentId
                                        join assign in assignments
                                        on emp.EmployeeNumber equals assign.EmployeeNumber
                                        where (departmentId == null || dept.DeptId == departmentId)
                                        select new CombinedEntities()
                                        {
                                            departmentName = dept.DeptName,
                                            projectName = proj.ProjectName,
                                            assignmentName = assign.AssignmentName,
                                            employeeName = emp.EmployeeName
                                        }).DistinctBy(d=>d.projectName);
            return projectAndAssignment;

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
            //foreach (var departmentSalary in totalSalaryPerDept)
            //{
            //    Console.WriteLine($"\t{departmentSalary.departmentId}\t\t{departmentSalary.totalSalary}");
            //}
            return totalSalaryPerDept;
        }
        public static void GetDetails<T>(IEnumerable<T> collectiondata)
        {
            foreach (var data in collectiondata)
            {
                Console.WriteLine(data?.ToString());
            }

        }

        public static void GetSpecificDetails(IEnumerable<CombinedEntities>? collection = null,IEnumerable<TotalSalaryByDepartment>? totalSalary = null)
        {
            if (collection != null)
            {
                foreach (var data in collection)
                {

                    Console.WriteLine($"\t{data.departmentName}\t{data.projectName}\t{data.assignmentName}\t{data.employeeName}");
                }
            }
            else if(totalSalary != null) { 
            foreach (var data in totalSalary)
            {
                Console.WriteLine($"\t{data.departmentId}\t\t{data.totalSalary}");
            }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        }
    }

