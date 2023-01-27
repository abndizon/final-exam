using FinalExam.Models;

namespace FinalExam.Conf {
    public class ApplicationContext
    {
        private List<Employee> empList;
        
        private static ApplicationContext instance = null;

        public static ApplicationContext Instance
        {
            get {
                if (instance == null) {
                    instance = new ApplicationContext();
                }
                return instance;
            }
        }

        public ApplicationContext()
        {
            this.empList = new List<Employee>();
        }

        public List<Employee> GetEmployees()
        {
            return this.empList;
        }
    }
}