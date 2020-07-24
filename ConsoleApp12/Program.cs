// By:
// Behzad Joulaei
// Mehwish Arif
// Syeda Rudsana Sarowatt Esha
// Jinal Patel


using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Operotors
{
    class Program
    {
        static void Main(string[] args)
        {
            List<customer> CustomerList = new List<customer>();
            CustomerList.Add(new customer(1, "Tom"));  //with  multiple orders
            CustomerList.Add(new customer(2, "John"));
            CustomerList.Add(new customer(3, "Sarah"));
            CustomerList.Add(new customer(4, "Bob"));  // with no order


            List<order> OrderList = new List<order>();
            OrderList.Add(new order(1, 1, 1));
            OrderList.Add(new order(2, 1, 3));
            OrderList.Add(new order(3, 1, 4));
            OrderList.Add(new order(4, 1, 3));
            OrderList.Add(new order(5, 1, 7));
            OrderList.Add(new order(6, 1, 9));

            OrderList.Add(new order(7, 2, 1000));
            OrderList.Add(new order(8, 3, 2));
            OrderList.Add(new order(9, 3, 5));
            OrderList.Add(new order(10, 3, 12));



            //Print a list of Customers in alphabetical order 
            Console.WriteLine($"Query 3");
            IEnumerable<customer> query3 = CustomerList.OrderBy(x => x.Name).ToList();
            foreach (customer item in query3)
            {
                Console.WriteLine(item.Name);
            }

            // Print a list of Orders in Customer # order. 
            Console.WriteLine($"Query 4");
            IEnumerable<order> query4 = OrderList.OrderBy(x => x.OrderNumber).ToList();
            foreach (order item in query4)
            {
                Console.WriteLine(item.OrderNumber);
            }


            
            //Print a list of Orders grouped by Customer # and in ascending Order # sequence
            var query5 = OrderList.GroupBy(x => x.CustomerNumber).OrderBy(x => x.Key).ToList();

            Console.WriteLine($"Query 5");
            foreach (var item in query5)
            {
                Console.WriteLine($"CustomerNumber: {item.Key}");
                foreach (var ord in item)
                {
                    Console.WriteLine($"\t{ord.OrderNumber }, {ord.NumberOfItems }");
                }
            }

            //Print a list of orders that includes the Customer name. 
            var query6 = OrderList.Join(CustomerList, o => o.CustomerNumber, c => c.CustomerNumber, (o, c) => new {c.Name,o.CustomerNumber,o.OrderNumber,o.NumberOfItems });
            Console.WriteLine($"Query 6");
            foreach (var ord in query6)
            {
                Console.WriteLine($"{ord.OrderNumber}, {ord.NumberOfItems } , {ord.CustomerNumber}, {ord.Name}");
            }


            //Print a list of orders grouped by Customer with the Customer Name
            var query7 = from ord in OrderList
                         join cust in CustomerList on ord.CustomerNumber equals cust.CustomerNumber
                         group ord by cust.Name into newGroup
                         select newGroup;
            Console.WriteLine($"Query 7");
            foreach (var nameGroup in query7)
            {
                Console.WriteLine($"CustomerName: {nameGroup.Key}");
                foreach (var ord in nameGroup)
                {
                    Console.WriteLine($"\t{ord.OrderNumber }, {ord.NumberOfItems }");
                }
            }


            //Print a list of orders in grouped by Customer and in reverse sequence based on Customer Name. 
            var query8 = from ord in OrderList
                         join cust in CustomerList on ord.CustomerNumber equals cust.CustomerNumber
                         orderby cust.Name descending
                         group ord by cust.Name into newGroup
                         select newGroup;

            Console.WriteLine($"Query 8");
            foreach (var nameGroup in query8)
            {
                Console.WriteLine($"CustomerName: {nameGroup.Key}");
                foreach (var ord in nameGroup)
                {
                    Console.WriteLine($"\t{ord.OrderNumber }, {ord.NumberOfItems }");
                }
            }



        }
    }


    public class customer
    {
        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public customer(int customerNum, string name)
        {
            CustomerNumber = customerNum;
            Name = name;
        }

    }
    public class order
    {
        public int OrderNumber { get; set; }
        public int CustomerNumber { get; set; }
        public int NumberOfItems { get; set; }
        public order(int orderNumber, int customerNumber, int numberOfItems)
        {
            OrderNumber = orderNumber;
            CustomerNumber = customerNumber;
            NumberOfItems = numberOfItems;
        }

    }

}