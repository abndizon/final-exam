using FinalExam.Services;
using FinalExam.Models;
using System.Text.Json;

namespace FinalExam.Commands {
    public class BuildReport {

        EmployeeService employeeService;
        List<Employee> normalEmployees;
        List<SalesEmployee> salesEmployees;
        List<Object> formattedEmployee;
        List<Object> formattedSalesEmployee;
        float totalSales;
        float totalCommission;

        public BuildReport() {
            employeeService = new EmployeeService();
        }

        public string Execute() {
            this.normalEmployees = employeeService.GetAllNormalEmployees();
            this.salesEmployees = employeeService.GetAllSalesEmployees().Cast<SalesEmployee>().ToList();

            FormatData();

            var seralizerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            
            Dictionary<string, object> jsonObj = new Dictionary<string, object>();
            jsonObj.Add("employees", formattedEmployee);
            jsonObj.Add("salesEmployees", formattedSalesEmployee);

            SalesEmployee se = salesEmployees.FirstOrDefault(x => x.Sales.Count > 0);

            if (se != null) {
                jsonObj.Add("totalSales", totalSales);
                jsonObj.Add("totalCommission", totalCommission);
            }
            
            string jsonString = JsonSerializer.Serialize(jsonObj, seralizerOptions);
            return jsonString;
        }

        private void FormatData() {
            formattedEmployee = new List<object>();
            formattedSalesEmployee = new List<object>();
            totalSales = 0.0f;
            totalCommission = 0.0f;

            formattedEmployee = normalEmployees.Select(e => new { e.Id, e.EmployeeNumber, e.FirstName, e.LastName, e.BaseSalary }).ToList<Object>();
            formattedSalesEmployee = salesEmployees.Select(se => new { se.Id, se.EmployeeNumber, se.FirstName, se.LastName, se.BaseSalary, se.Commission }).ToList<Object>();
            
            // Compute total sales and commission
            totalSales = salesEmployees.Sum(x => x.ComputeTotalSale());
            totalCommission = salesEmployees.Sum(x => (x.ComputeTotalSale() * x.Commission));
        }
    }
}