using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prototype pattern de amaç nesne üretim maliyetlerini minimize etmektir. Her zaman kullanılamaz. İhtiyaç dahilinde kullanılır. Temel bir sınıf üzerinden prototype oluşturulur ve klonlanmış olur
            //Temel nesne prototype dir. Burada Person temel nesnedir.
            //Perfomans artışı sağlar

            Customer customer1 = new Customer
            {
                FirstName = "Kadir",
                LastName = "Muşuk",
                City = "Bartın",
                Id = 1
            };

            Customer customer2 = (Customer) customer1.Clone();
            customer2.FirstName = "Ahmet";

            Console.WriteLine(customer1.FirstName);
            Console.ReadLine();

        }

        public abstract class Person
        {
            //Temel nesneyi prototype haline getirebilmek için onu soyut bir clone metodundan besleniyor olması gerekir.

            public abstract Person Clone();
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Customer : Person
        {
            public string City { get; set; }

            public override Person Clone()
            {
                //Customer ı clonelama işlemi
                return (Person)MemberwiseClone();
            }
        }

        public class Employee : Person
        {
            public decimal Salary { get; set; }

            public override Person Clone()
            {
                //Employee clonelama işlemi
                return (Person)MemberwiseClone();
            }
        }
    }
}
