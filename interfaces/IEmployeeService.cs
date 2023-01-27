using FinalExam.Models;

namespace FinalExam.Interfaces {
    public interface IEmployeeService {
        public List<Employee> GetAll();
        public List<Employee> GetAllSalesEmployees();
        public List<Employee> GetAllNormalEmployees();
        public Employee Save(Employee e);
        public void Delete(Employee e);
        public void AddSale(SalesEmployee e, Sale s);
    }
}