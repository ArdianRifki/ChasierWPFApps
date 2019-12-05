namespace CRUDBC32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.tb_m_user",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Roles_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_m_role", t => t.Roles_ID)
                .Index(t => t.Roles_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_user", "Roles_ID", "dbo.tb_m_role");
            DropIndex("dbo.tb_m_user", new[] { "Roles_ID" });
            DropTable("dbo.tb_m_user");
            DropTable("dbo.tb_m_role");
        }
    }
}
