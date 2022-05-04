using DataAccessLayer.Models;
using DomainLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    
        public class EmployeeRepository : IEmployeeRepository
        {
            public async Task<List<EmployeeDto>> GetEmployees()
            {
                

                using (EmployeeDbContext dbContext = new EmployeeDbContext())
                {
                    var employees = await dbContext.Employees.ToListAsync();

                    List<EmployeeDto> domainModels = new List<EmployeeDto>();
                    
                    foreach (var emp in employees)
                    {
                        domainModels.Add(new EmployeeDto
                        {
                            Id = emp.Id,
                            Name = emp.Name,
                            Salary = emp.Salary,
                            DepartmentId = emp.DepartmentId
                        });
                    }
                    return domainModels;
                }
            }

            public async Task InsertEmployee(EmployeeDto employee)
            {
                
                using (EmployeeDbContext dbContext = new EmployeeDbContext())
                {
                    
                    var dbModel = new Employee
                    {
                        Name = employee.Name,
                        Salary = employee.Salary,
                        DepartmentId = employee.DepartmentId
                    };

                    dbContext.Employees.Add(dbModel);
                    await dbContext.SaveChangesAsync();
                }
            }

            public async Task UpdateEmployee(EmployeeDto employee)
            {
                using (EmployeeDbContext dbContext = new EmployeeDbContext())
                {
                    var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

                    findEmployee.Name = employee.Name;
                    findEmployee.Salary = employee.Salary;

                    dbContext.Employees.Update(findEmployee);
                    await dbContext.SaveChangesAsync();
                }

                
            }

            public async Task DeleteEmployee(int id)
            {
                using (EmployeeDbContext dbContext = new EmployeeDbContext())
                {
                    var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
                    dbContext.Employees.Remove(findEmployee);
                    await dbContext.SaveChangesAsync();
                }
            }

            public async Task<EmployeeDto> GetEmployee(int id)
            {

                using (EmployeeDbContext dbContext = new EmployeeDbContext())
                {
                    var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

                    EmployeeDto domainModel = new EmployeeDto
                    {
                        DepartmentId = employee.DepartmentId,
                        Id = employee.Id,
                        Name = employee.Name,
                        Salary = employee.Salary
                    };

                    return domainModel;
                }
            }


        }
    }


