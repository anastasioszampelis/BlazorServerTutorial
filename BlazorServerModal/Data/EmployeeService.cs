using BlazorServerModal.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerModal.Data
{
    public class EmployeeService
    {
        public Employee[] SelectedEmployees { get; set; }
        private static readonly string[] FirstNames = new[]
        {
            "Anastasios", "Konstantinos", "Panagiotis", "Kyriakos", "Alexis", "Fofi", "Dimitris", "Gianis"
        };
        private static readonly string[] LastNames = new[]
        {
            "Zampelis", "Katakouzinos", "Giannakis", "Mitsotakis", "Tsipras", "Gennimata", "Koutsoumpas", "Varoufakis"
        };

        public Task<Tuple<bool, string>> SaveEmployeeData(Employee employee)
        {
            try
            {
                var selectedEmployee = SelectedEmployees
                    .Where(d => d.Id == employee.Id)
                    .FirstOrDefault();
                if (selectedEmployee == null)
                {
                    throw new Exception("Employee has not been found");
                }
                else
                {
                    selectedEmployee.FirstName = employee.FirstName;
                    selectedEmployee.LastName = employee.LastName;
                    selectedEmployee.Active = employee.Active;
                    var response = new Tuple<bool, string>(true, "Employee's data have been saved succesfully!");
                    return Task.FromResult(response);
                }
            }
            catch(Exception ex)
            {
                var response = new Tuple<bool, string>(false, ex.Message);
                return Task.FromResult(response);
            }

        }

        public Task<Tuple<bool, string>> DeleteEmployee(int employeeId)
        {
            try
            {
                var selectedEmployee = SelectedEmployees
                    .Where(d => d.Id == employeeId)
                    .FirstOrDefault();

                if(selectedEmployee == null)
                {
                    throw new Exception("Employee ha not been found.");
                }
                else
                {
                    SelectedEmployees = SelectedEmployees
                        .Where(d => d.Id != employeeId)
                        .ToArray();
                    return Task.FromResult(new Tuple<bool, string>(true, "Employee deleted succesfully!"));
                }
            }
            catch(Exception ex)
            {
                return Task.FromResult(new Tuple<bool, string>(false, ex.Message));
            }

        }

        public Task<Employee> GetEmployeeAsync(int employeeId)
        {
            var selectedEmployee = SelectedEmployees
                .Where(d => d.Id == employeeId)
                .FirstOrDefault();
            return Task.FromResult(selectedEmployee);
        }
        public Task<Employee[]> GetEmployeesAsync()
        {
            if (SelectedEmployees == null || !SelectedEmployees.Any())
            {
                var rng = new Random();
                SelectedEmployees = Enumerable.Range(1, 54).Select(index => new Employee
                {
                    Id = index,
                    FirstName = FirstNames[rng.Next(FirstNames.Length)],
                    LastName = LastNames[rng.Next(LastNames.Length)],
                    Email = $"test{index}@test.gr",
                    Active = rng.Next(2) == 1 ? true : false
                }).ToArray();
            }

            return Task.FromResult(SelectedEmployees);
        }
    }
}
