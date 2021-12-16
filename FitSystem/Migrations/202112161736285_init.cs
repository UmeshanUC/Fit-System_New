namespace FitSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchCode = c.Int(nullable: false),
                        Name = c.String(),
                        Adress = c.String(),
                        Telephone = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.BranchCode);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        NIC = c.String(nullable: false, maxLength: 128),
                        EpfNo = c.String(),
                        BankAccount = c.Int(nullable: false),
                        OtherFacts = c.String(),
                        BaseSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bonus = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.NIC)
                .ForeignKey("dbo.People", t => t.NIC, cascadeDelete: true)
                .Index(t => t.NIC);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        NIC = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        WorkRoleID = c.Int(nullable: false),
                        Email = c.String(),
                        Gender = c.String(),
                        Address = c.String(),
                        JoinedDate = c.DateTime(nullable: false),
                        Telephone = c.String(),
                        Pic = c.Binary(),
                        TodayPresence = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NIC)
                .ForeignKey("dbo.WorkRoles", t => t.WorkRoleID, cascadeDelete: true)
                .Index(t => t.WorkRoleID);
            
            CreateTable(
                "dbo.WorkRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        ItemCode = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        PurchasedDate = c.String(),
                        UnitValue = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCode);
            
            CreateTable(
                "dbo.LoginLogs",
                c => new
                    {
                        NIC = c.String(nullable: false, maxLength: 128),
                        LoggedTs = c.DateTime(nullable: false),
                        LogOutTs = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.NIC, t.LoggedTs });
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        NIC = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false),
                        Passwd = c.String(nullable: false),
                        PermissionLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NIC)
                .ForeignKey("dbo.Permissions", t => t.PermissionLevel, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.NIC, cascadeDelete: true)
                .Index(t => t.NIC)
                .Index(t => t.PermissionLevel);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermID = c.Int(nullable: false),
                        PermName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PermID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        NIC = c.String(nullable: false, maxLength: 128),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Package = c.String(),
                    })
                .PrimaryKey(t => t.NIC)
                .ForeignKey("dbo.People", t => t.NIC, cascadeDelete: true)
                .Index(t => t.NIC);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Benefits = c.String(),
                        Duration = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        Details = c.String(),
                        Supplier = c.String(),
                        UnitPrice = c.Int(nullable: false),
                        InStockQty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionCode = c.String(nullable: false, maxLength: 128),
                        SectionName = c.String(),
                        Description = c.String(),
                        SectionManager = c.String(),
                    })
                .PrimaryKey(t => t.SectionCode);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        Name = c.String(),
                        Agent = c.String(),
                        Telephone = c.Int(nullable: false),
                        Adress = c.String(),
                        DatePartnered = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Training_Plan",
                c => new
                    {
                        PlanCode = c.String(nullable: false, maxLength: 128),
                        Shedule = c.String(),
                        Diet = c.String(),
                    })
                .PrimaryKey(t => t.PlanCode);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false),
                        PayMethod = c.String(),
                        Value = c.Int(nullable: false),
                        Date = c.String(),
                        Time = c.String(),
                        Payee = c.String(),
                        Type = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.TransactionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "NIC", "dbo.People");
            DropForeignKey("dbo.Logins", "NIC", "dbo.People");
            DropForeignKey("dbo.Logins", "PermissionLevel", "dbo.Permissions");
            DropForeignKey("dbo.Employees", "NIC", "dbo.People");
            DropForeignKey("dbo.People", "WorkRoleID", "dbo.WorkRoles");
            DropIndex("dbo.Members", new[] { "NIC" });
            DropIndex("dbo.Logins", new[] { "PermissionLevel" });
            DropIndex("dbo.Logins", new[] { "NIC" });
            DropIndex("dbo.People", new[] { "WorkRoleID" });
            DropIndex("dbo.Employees", new[] { "NIC" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Training_Plan");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Sections");
            DropTable("dbo.Products");
            DropTable("dbo.Packages");
            DropTable("dbo.Members");
            DropTable("dbo.Permissions");
            DropTable("dbo.Logins");
            DropTable("dbo.LoginLogs");
            DropTable("dbo.Inventories");
            DropTable("dbo.WorkRoles");
            DropTable("dbo.People");
            DropTable("dbo.Employees");
            DropTable("dbo.Branches");
        }
    }
}
