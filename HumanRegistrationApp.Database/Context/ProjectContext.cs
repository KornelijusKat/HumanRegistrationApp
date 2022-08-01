using HumanRegistrationApp.Database.Model;
using Microsoft.EntityFrameworkCore;


namespace HumanRegistrationApp.Database.Context
{
    public class ProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
    }
}