using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ManytoMany_Assignment10
{
	class Store
	{
		public int Id { get; set; }

		public string Name { get; set; }

	}

	class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public double Price { get; set; }

		public List<Order> Orders { get; set; }
	}

	class Order
	{
		public int Id { get; set; }

		public string OrderDetails { get; set; }

		public Product OrderedProduct { get; set; }


	}
	class Registration
	{
		public int Id { get; set; }

		public Product RegisteredProduct { get; set; }

		public Order RegisteredOrder { get; set; }

		public DateTime RegistrationDate { get; set; }

	}

	class ProductRegistrationContext : DbContext
	{

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }

		public DbSet<Registration> Registrations { get; set; }

		string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Assignment10;Trusted_Connection=True;MultipleActiveResultSets=true";
		protected override void OnConfiguring(DBContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			using (ProductRegistrationContext context = new ProductRegistrationContext())
			{
				context.Database.EnsureCreated();
				Store store = new Store { Id=001, Name="Coscto"};
				Product product = new Product { Id=123, Name="vita coco"};
				Order order = new Order { Id = 12, OrderDetails = "2", OrderedProduct= product };
				Registration register = new Registration
				{
					Id=1234,
					RegisteredProduct = product,
					RegisteredOrder= order,
					RegistrationDate = DateTime.Now

				};
				//context.Store.Add(store);
				context.Products.Add(product);
				context.Orders.Add(order);
				context.Registrations.Add(register);
				context.SaveChanges();

				context.Database.EnsureCreated();
				Store store1 = new Store { Id = 001, Name = "Sam" };
				Product product1 = new Product { Id = 123, Name = "coke" };
				Order order1 = new Order { Id = 12, OrderDetails = "2", OrderedProduct =  product1};
				Registration register1 = new Registration
				{
					Id = 1235,
					RegisteredProduct = product,
					RegisteredOrder = order,
					RegistrationDate = DateTime.Now

				};
				//context.Stores.Add(store1);
				context.Products.Add(product1);
				context.Orders.Add(order1);
				context.Registrations.Add(register1);
				context.SaveChanges();

				// Display all orders where a product is sold

				List<Order> reOrder = context.Registrations
											.Include(e => e.RegisteredOrder)
											.Where(e => e.Product == "coke")
											.FirstOrDefault();
				// For a given product, find the order where it is sold the maximum.

				Product readproduct = context.Registrations
					.Include(i = i.register).Where(s => s.Product = "coke")
					.OrderbyDescending(p => p.Store)
					.First();



			}
		}
	}
}
