namespace FinalExam.Models {
    public class SalesEmployee : Employee {
        public float Commission { get; private set; }
        public List<Sale> Sales { get; private set; }

        public SalesEmployee(int id, string firstName, string lastName, string employeeNumber, float baseSalary, float commission) 
            : base(id, firstName, lastName, employeeNumber, baseSalary)    
        {
            this.Commission = commission;
            this.Sales = new List<Sale>();
        }

        public float GetSalary() {
            float salary = base.BaseSalary + (Commission * ComputeTotalSale());

            return salary;
        }

        public void AddSale(Sale sale) {
            this.Sales.Add(sale);
        }
        
        public float ComputeTotalSale() {
            float totalSales = 0.0f;

            foreach (var sale in Sales) {
                totalSales += sale.Amount;
            }

            return totalSales;
        }
    }
}