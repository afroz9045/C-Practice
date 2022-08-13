// See https://aka.ms/new-console-template for more information
using AutoMapper;
using EmployeeRecordBook.Configurations;
using EmployeeRecordBook.Infrastructure.Data;
using EmployeeRecordBook.Infrastructure.Repositories;

Console.WriteLine("Hello, World!");

#region Configure and Register AutoMapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();

#endregion



//IDepartmentRepository departmentRepository = new DepartmentRepository();
//await departmentRepository.CreateAsync(new Department() { Id = 1, Name = "HR"});


//await departmentRepository.CreateRangeAsync(
//   new List<Department>
//   {
//      new Department() { Id = 2, Name =  "IT" },
//      new Department() { Id = 3, Name = "Accounting" }
//   }
//   );


using (var employeeContext = new EmployeeContext())
{
   IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);
    //await employeeRepository.CreateRangeAsync(
    //new List<Employee>
    //{
    //    new Employee() {Name="Sunil Kumar",Email="sunil@global.com",Salary=10600m,DepartmentId=2,},
    //    new Employee() {Name="mohan sachit",Email="mohansachit@global.com",Salary=12600m,DepartmentId=1},
    //    new Employee() {Name="mohit arya",Email="mohitarya@global.com",Salary=15600m,DepartmentId=2},
    //    new Employee() {Name="parmeshwar savrate",Email="parmeshwarsavrate@global.com",Salary=16600m,DepartmentId=2},
    //    new Employee() {Name="samvidth",Email="samvidth@gmail.com",Salary=13600m,DepartmentId=3},
    //    new Employee() {Name="harish",Email="harish@global.com",Salary=14600m,DepartmentId=1},
    //    new Employee() {Name="mahindra",Email="mahindra@global.com",Salary=13300m,DepartmentId=1},
    //    new Employee() {Name="raj",Email="raj@global.com",Salary=11600m,DepartmentId=3},
    //    new Employee() {Name="rahul",Email="rahul@global.com",Salary=10600m,DepartmentId=2}
    //});

    //var anilVm = new EmployeeVm
    //{
    //    Name = "Anil Kumar",
    //    Email = "anil@global.com",
    //    Salary = 10000m,
    //    DepartmentId = 1
    //};
    // Do validation and other formatting on View Model received from UI.

    // Transform your VM to Entity which can be saved to DB.
    //var amilEntity = mapper.Map<EmployeeVm, Employee>(anilVm);

  // var anil = await employeeRepository.CreateAsync(amilEntity);
   //var sunil = await employeeRepository.CreateAsync(new Employee
   //{
   //   Name = "Sunil Kumar",
   //   Email = "sunil@global.com",
   //   Salary = 10600m,
   //   DepartmentId = 2
   //});
  // Console.WriteLine($"Created Employees: {anil.Id} {anil.Name}, {sunil.Id} {sunil.Name}");
    Console.WriteLine("\nsorted employee data:");
    var employees = await employeeRepository.GetEmployeesAsync(pageIndex:1,pageSize:4,sortOrder:"asc",sortField:"Name");
    foreach(var empData in employees)
    {
        Console.WriteLine($"{empData.Name}\t{empData.Email}\t{empData.Id}\t{empData.Salary}\t{empData.DepartmentName}");
    }
    //Console.WriteLine($"Total Employee Records: {employees.Count()}");

   // var updatedAnilData = new Employee
   //{
   //   Name = "Anil Kumar",
   //   Email = "anil@email.com",
   //   Salary = 12000m,
   //   DepartmentId = 1
   //};
   //var udatedEmployee = await employeeRepository.UpdateAsync(anil.Id, updatedAnilData);
   //Console.WriteLine($"Updated Employee: {udatedEmployee.Id} {udatedEmployee.Name} {udatedEmployee.Email} {udatedEmployee.Salary}");

   //await employeeRepository.DeleteAsync(sunil?.Id ?? 0);

   //var deletedRecord = await employeeRepository.GetEmployeeAsync(sunil?.Id ?? 0);
   //Console.WriteLine($"Was record deleted successfully? {deletedRecord == null: true ? false}");
}
