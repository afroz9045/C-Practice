using Pagging.Infrastructure.Data;
using System.Reflection.Metadata.Ecma335;

namespace Pagging.Infrastructure
{
    public class InMemoryData
    {
           
        public static List<Employee> data = new List<Employee>()
        {
            new Employee{EmpId = 1,EmpName = "Mohd Afroz Khan"},
            new Employee{EmpId = 2,EmpName = "Shabaz Khan"},
            new Employee{EmpId = 3,EmpName = "Sarfaraz Khan"},
            new Employee{EmpId = 4,EmpName = "ismail"},
            new Employee{EmpId = 5,EmpName = "yousuf Khan"},
            new Employee{EmpId = 6,EmpName = "fardeen Khan"},
            new Employee{EmpId = 7,EmpName = "hamza"},
            new Employee{EmpId = 8,EmpName = "anas"},
            new Employee{EmpId = 9,EmpName = "Mohd idress"},
            new Employee{EmpId = 10,EmpName = "syed zaki ahmed"}
        };

    public static IEnumerable<Employee> GetEmployees(int numberOfRecords,int pageNumber= 1)
        {
            var empData = from emp in data.Skip((pageNumber - 1) * numberOfRecords).Take(numberOfRecords)
                          select emp;
        return empData;
        }
    }
}