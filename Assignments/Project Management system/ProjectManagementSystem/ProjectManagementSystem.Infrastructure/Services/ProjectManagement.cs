using Pms.Core.Entities;
using ProjectManagementSystem.Core.Contracts;
using System.Text.Json;
using static ProjectManagementSystem.Infrastructure.Data.ProjectManagementDataInMemory;
using System.Text;

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

        public List<Project> GetProjectsByDepartmentName(string departmentName)
        {
            var projectFilteredData = from project in projects
                                      join depart in department
                                      on project.DepartmentId equals depart.DeptId
                                      where depart.DeptName == departmentName
                                      select project;
            return projectFilteredData.ToList();
            
        }

        public IEnumerable<Employee> GetEmployeesByDeptId(int deptId)
        {
            var employeeFilteredData = from employee in employees
                                       where employee.DepartmentId == deptId
                                       select employee;
            return employeeFilteredData;
        }

        public static void GetDetails<T>(IEnumerable<T> collectiondata)
        {
            foreach (var data in collectiondata)
            {
                Console.WriteLine(data?.ToString());
            }

        }

        public static void GetSpecificDetails<T>(IEnumerable<T> collection) 
        {
            foreach (var data in collection)
            {
                Console.WriteLine(data);
            }
        }
    }
}
