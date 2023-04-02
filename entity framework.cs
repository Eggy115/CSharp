using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

class Program {
    static void Main(string[] args) {
        using (var db = new MyDbContext()) {
            var customers = db.Customers.ToList();
            foreach (var c in customers) {
                Console.WriteLine(c.Name);
            }
        }
    }
}

class MyDbContext : DbContext {
    public DbSet<Customer> Customers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        options.UseSqlServer("Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;");
    }
}

class Customer {
    public int Id { get; set; }
    public string Name { get; set; }
}
