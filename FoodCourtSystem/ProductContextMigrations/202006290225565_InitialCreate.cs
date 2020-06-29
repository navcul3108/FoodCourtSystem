namespace FoodCourtSystem.ProductContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItemModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        TotalMoney = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CartModels", t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.CartModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        OwnerName = c.String(),
                        TotalMoney = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItemModels", "ID", "dbo.ProductModels");
            DropForeignKey("dbo.CartItemModels", "ID", "dbo.CartModels");
            DropIndex("dbo.CartItemModels", new[] { "ID" });
            DropTable("dbo.CartModels");
            DropTable("dbo.CartItemModels");
        }
    }
}
