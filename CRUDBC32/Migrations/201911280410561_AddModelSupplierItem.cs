namespace CRUDBC32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelSupplierItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stock = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                        Supplier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_item", "Supplier_Id", "dbo.Suppliers");
            DropIndex("dbo.tb_m_item", new[] { "Supplier_Id" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.tb_m_item");
        }
    }
}