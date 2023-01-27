using FinalExam.Interfaces;
using FinalExam.Models;
using FinalExam.Conf;

namespace FinalExam.Services {
    public class EmployeeService : IEmployeeService
    {
        private ApplicationContext appInstance;
        private List<Employee> employeeList;

        public EmployeeService() {
            appInstance = ApplicationContext.Instance;
            employeeList = appInstance.GetEmployees();
        }

        public void AddSale(SalesEmployee e, Sale s)
        {
            e.AddSale(s);
        }

        public void Delete(Employee e)
        {
            employeeList.Remove(e);
        }

        public List<Employee> GetAll()
        {
            return this.employeeList;
        }

        public List<Employee> GetAllSalesEmployees()
        {
            List<Employee> salesEmployees = new List<Employee>();
            
            foreach (Employee employee in employeeList) {
                if (employee.GetType() == typeof(SalesEmployee)) {
                    SalesEmployee temp = (SalesEmployee) employee;
                    salesEmployees.Add(temp);
                }
            }

            return salesEmployees;
        }

        public List<Employee> GetAllNormalEmployees()
        {
            List<Employee> normalEmployees = new List<Employee>();
            
            foreach (Employee employee in employeeList) {
                if (employee.GetType() != typeof(SalesEmployee)) {
                    normalEmployees.Add(employee);
                }
            }

            return normalEmployees;
        }

        public Employee Save(Employee e)
        {
            employeeList.Add(e);
            return e;
        }
    }
}