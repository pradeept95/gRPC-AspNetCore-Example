using Application.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<EmailAddress> EmailAddress { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
    }
}
