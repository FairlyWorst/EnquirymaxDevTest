namespace EMaxDevTest.Data
{
    using EMaxDevTest.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.InMemory;

    public class EmaxContext : DbContext
    {
        public EmaxContext(DbContextOptions<EmaxContext> options) 
        : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }
    }
}
