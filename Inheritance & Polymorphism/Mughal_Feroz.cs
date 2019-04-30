using System;

namespace ConsoleApp1
{
    public class Employee
    {

        private string firstName;
        private string lastName;
        private int Number;
        private string ID;

        public Employee(string firstName, string lastName, int Number)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.Number = Number;
        }

        public string EmployeeName
        {
            get { return firstName + " " + lastName; }
            set { }
        }

        public int EmployeeNumber
        {
            set
            {
                int temp = new Int32();
                temp = Number;
                Number = value;
                if (!Number.ToString().Length.Equals(4))
                {
                    Number = temp;
                    Console.WriteLine("Cannot Change Number. The Employee Number should be exactly 4 digits.");
                }
            }
            get { return Number; }
        }

        public string EmployeeID
        {
            get { return ID + Char.ToLower(firstName[0]) + Char.ToLower(lastName[0]) + lastName[1] + lastName[2] + Number; }
            set { }
        }

        public virtual void Print()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Employee name: " + EmployeeName);
            Console.WriteLine("Employee ID: " + EmployeeID);
            Console.WriteLine("-----------------------------------------");
        }
    }

    public class Faculty : Employee
    {

        private string Code;

        public Faculty(string firstName, string lastName, int Number, string Code) : base(firstName, lastName, Number)
        {
            this.Code = Code;
        }

        public string EmployeeCode
        {
            get { return Code; }
            set { Code = value; }
        }

        public override void Print()
        {
            Console.WriteLine("Employee name: " + EmployeeName);
            Console.WriteLine("Employee ID: " + EmployeeID);
            Console.WriteLine("Department Code: " + EmployeeCode);
        }
    }


    public class Instructor : Faculty
    {

        private decimal Rate;

        public Instructor(string firstName, string lastName, int Number, string Code, decimal Rate) : base(firstName, lastName, Number, Code)
        {
            this.Rate = Rate;
        }

        public override void Print()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Employee name: " + EmployeeName);
            Console.WriteLine("Employee ID: " + EmployeeID);
            Console.WriteLine("Department Code: " + EmployeeCode);
            Console.WriteLine("Rate: " + Rate + "$/hour");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Employee[] emps = {
            new Employee( "Robert", "John", 6611 ),
            new Faculty("Sara", "Brown", 2010, "CSC"),
            new Instructor("Steven", "Hank", 3344, "ECE", 20)
        };

            foreach (Employee e in emps)
                e.Print();
            Console.WriteLine("-----------------------------------------");
            emps[0].EmployeeNumber = 331;
            Console.WriteLine(emps[0].EmployeeName + ": " + emps[0].EmployeeID);
        }
    }
}
