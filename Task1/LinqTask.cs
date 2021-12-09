using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {          
            return customers.Where(c => c.Orders.Count() > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.Select(customer => (customer, suppliers.Where(s =>
                                    s.Country == customer.Country && 
                                    s.City == customer.City))).ToList();

            return result;
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            var result = customers.Select(customer => (customer, suppliers.Where(s =>
                                     s.Country == customer.Country &&
                                     s.City == customer.City))).ToList();

            return result;
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Any(o => o.Total > limit));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var result = from customer in customers
                         where customer.Orders.Count() > 0
                         select ( customer, customer.Orders[0].OrderDate );

            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = (from customer in customers
                         where customer.Orders.Count() > 0
                         orderby customer.CompanyName ascending 
                         from order in customer.Orders.Take(1)
                         orderby order.OrderDate ascending                          
                         select (customer, customer.Orders[0].OrderDate));

            return result;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            var result = customers.Where(c =>
                            !c.Phone.Contains("(") ||
                            string.IsNullOrEmpty(c.Region) ||
                            !c.PostalCode.All(char.IsDigit));

            return result;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            var result = products.GroupBy(product => product.Category)
                        .Select(group => new Linq7CategoryGroup
                        {
                            Category = group.Key,
                            UnitsInStockGroup = group.GroupBy(product => product.UnitsInStock)
                                                .Select(group2 => new Linq7UnitsInStockGroup
                                                {
                                                    UnitsInStock = group2.Key,
                                                    Prices = group2.Select(product => product.UnitPrice)
                                                })
                        });

            return result;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}