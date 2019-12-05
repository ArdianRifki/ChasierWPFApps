namespace CRUDBC32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelTransaction : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_transactionitem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        SubTotal = c.Int(nullable: false),
                        Items_Id = c.Int(),
                        Transactions_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_m_item", t => t.Items_Id)
                .ForeignKey("dbo.tb_m_transaction", t => t.Transactions_Id)
                .Index(t => t.Items_Id)
                .Index(t => t.Transactions_Id);
            
            CreateTable(
                "dbo.tb_m_transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Total = c.Int(nullable: false),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_transactionitem", "Transactions_Id", "dbo.tb_m_transaction");
            DropForeignKey("dbo.tb_m_transactionitem", "Items_Id", "dbo.tb_m_item");
            DropIndex("dbo.tb_m_transactionitem", new[] { "Transactions_Id" });
            DropIndex("dbo.tb_m_transactionitem", new[] { "Items_Id" });
            DropTable("dbo.tb_m_transaction");
            DropTable("dbo.tb_m_transactionitem");
        }
    }
}
