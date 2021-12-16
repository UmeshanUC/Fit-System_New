namespace FitSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _long : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "BankAccount", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "BankAccount", c => c.Int(nullable: false));
        }
    }
}
