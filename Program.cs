using FinalExam.Models;
using FinalExam.Services;
using FinalExam.Interfaces;
using FinalExam.Commands;

namespace FinalExam {
    public class Program {
        static void Main(string[] args)
        {   
            IEmployeeService employeeService = new EmployeeService();
            List<Employee> myEmpList = employeeService.GetAll();
            BuildReport cmd = new BuildReport();

            bool quitProgram = false;

            do {
                Console.WriteLine("============ MAIN MENU ============");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1 - Display All Employees");
                Console.WriteLine("2 - Create Employee");
                Console.WriteLine("3 - Delete Employee");
                Console.WriteLine("4 - Add Sale");
                Console.WriteLine("5 - Quit");
                Console.WriteLine("===================================");     
                Console.Write("Enter number: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    if (myEmpList.Count > 0)
                    {
                        List<Employee> normalEmpList = employeeService.GetAllNormalEmployees();
                        Console.WriteLine("DISPLAYING EMPLOYEES..");
                        foreach (Employee e in normalEmpList) {
                            DisplayEmployee(e);
                            Console.WriteLine($"Base Salary: {e.GetSalary()}");
                        }

                        List<Employee> salesEmpList = employeeService.GetAllSalesEmployees();
                        if (salesEmpList.Count > 0) {
                            Console.WriteLine("\nSALES EMPLOYEES: ");
                            foreach (var se in salesEmpList) {
                                DisplayEmployee(se);
                                SalesEmployee temp = (SalesEmployee) se;
                                Console.WriteLine($"Base Salary: {temp.GetSalary()}");
                                Console.WriteLine($"Commission: {temp.Commission}");
                            }
                        }

                        /* TESTING OF JSON STRING OUTPUT
                        Console.WriteLine("============== JSON ===============");
                        Console.WriteLine(cmd.Execute());
                        */
                    }
                    else {
                        Console.WriteLine("Employee List is currently empty");
                    }
                }
                else if (choice == 2) {
                    bool goBack = false;
                    
                    do {
                        Console.WriteLine("========== EMPLOYEE MENU ==========");
                        Console.WriteLine("What type of Employee?");
                        Console.WriteLine("1 - Normal Employee");
                        Console.WriteLine("2 - Sales Employee");
                        Console.WriteLine("3 - Go Back");
                        Console.WriteLine("===================================");
                        Console.Write("Enter number: ");

                        int empChoice = Convert.ToInt32(Console.ReadLine());
                        int id;

                        // ID assignment
                        if (myEmpList.Count > 0) {
                            id = myEmpList.Last().Id + 1;
                        }
                        else {
                            id = 1;
                        }

                        if (empChoice == 1 || empChoice == 2) {
                            Console.Write("Enter First Name: ");
                            string firstName = Console.ReadLine();

                            Console.Write("Enter Last Name: ");
                            string lastName = Console.ReadLine();

                            Console.Write("Enter Employee Number: ");
                            string empNum = Console.ReadLine();

                            Console.Write("Enter Base Salary: ");
                            float baseSalary = float.Parse(Console.ReadLine());

                            if (empChoice == 1) {
                                Employee emp = new Employee(id, firstName, lastName, empNum, baseSalary);
                                employeeService.Save(emp);
                            }
                            else if (empChoice == 2) {
                                Console.Write("Enter Commission: ");
                                float commission = float.Parse(Console.ReadLine());

                                SalesEmployee salesEmp = new SalesEmployee(id, firstName, lastName, empNum, baseSalary, commission);
                                employeeService.Save(salesEmp);
                            }

                            Console.WriteLine($"EMPLOYEE ADDED");
                        }
                        else if (empChoice == 3) {
                            goBack = true;
                        }
                        else {
                            Console.WriteLine("ERROR. Invalid Choice");
                        }
                    } while (!goBack);  
                }
                else if (choice == 3) {
                    Console.Write("Enter ID of Employee to delete: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Employee emp = FindEmployee(myEmpList, id);

                    if (emp != null)
                    {
                        employeeService.Delete(emp);
                        Console.WriteLine($"EMPLOYEE ID {id} DELETED");
                    }
                }
                else if (choice == 4) {
                    Console.Write("Enter ID of Sales Employee: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Employee emp = FindEmployee(myEmpList, id);

                    if (emp != null)
                    {
                        if (employeeService.GetAllSalesEmployees().Contains(emp)) 
                        {
                            SalesEmployee temp = (SalesEmployee) emp;
                            Console.WriteLine("ADDING SALE..");
                            Console.Write("Enter Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Amount: ");
                            float amount = float.Parse(Console.ReadLine());

                            Sale sale = new Sale(name, amount);
                            employeeService.AddSale(temp, sale);
                            Console.WriteLine($"SALE ADDED");
                        }
                        else {
                            Console.WriteLine($"ERROR. ID {id} is not a Sales Employee.");
                        }
                    }
                }
                else if (choice == 5) {
                    quitProgram = true;
                }
                else {
                    Console.WriteLine("ERROR. Invalid Choice");
                }
            } while (!quitProgram);
        }

        public static void DisplayEmployee(Employee e) {
            Console.WriteLine("----------------------");
            Console.WriteLine($"ID: {e.Id}");
            Console.WriteLine($"Full Name: {e.FirstName} {e.LastName}");
            Console.WriteLine($"Employee Number: {e.EmployeeNumber}");
        }

        public static Employee FindEmployee(List<Employee> empList, int id) {
            Employee emp = empList.SingleOrDefault(x => x.Id == id);

            if (emp == null)
            {
                Console.WriteLine($"ERROR. Employee ID {id} not found.");
            }

            return emp;
        }
    }
}
