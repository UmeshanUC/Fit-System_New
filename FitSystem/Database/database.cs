using FitSystem.Models;
using System.Data.Entity;
using System.Threading;

namespace FitSystem.Database
{
    
    public class FitDb : DbContext
    {
        public FitDb() : base("FIT_db")
        { }

        public DbSet<Login> LoginSet { get; set; }
        public DbSet<LoginLog> LoginLogSet { get; set; }
        public DbSet<WorkRole> WorkRoleSet { get; set; }
        public DbSet<Permissions> PermissionSet { get; set; }
        public DbSet<Branches> BranchSet { get; set; }
        public DbSet<Employee> EmployeeSet { get; set; }
        public DbSet<Person> PersonSet { get; set; }
        public DbSet<Inventory> ItemSet { get; set; }
        public DbSet<Member> MemberSet { get; set; }
        public DbSet<Package> PackageSet { get; set; }
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<Section> SectionSet { get; set; }
        public DbSet<Supplier> SupplierSet { get; set; }
        public DbSet<Training_Plan> TrainingProfileSet { get; set; }
        public DbSet<Transactions> TransactionSet { get; set; }
    }
    

}
