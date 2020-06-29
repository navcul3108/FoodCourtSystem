namespace FoodCourtSystem.MenuContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IniialCreate : DbMigration
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
                        ProductID = c.String(maxLength: 128),
                        CartID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CartModels", t => t.CartID)
                .ForeignKey("dbo.ProductModels", t => t.ProductID)
                .Index(t => t.ProductID)
                .Index(t => t.CartID);
            
            CreateTable(
                "dbo.CartModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        OwnerName = c.String(),
                        TotalMoney = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        UnitPrice = c.Int(nullable: false),
                        ImageName = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 200),
                        Category_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryModels", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItemModels", "ProductID", "dbo.ProductModels");
            DropForeignKey("dbo.ProductModels", "Category_ID", "dbo.CategoryModels");
            DropForeignKey("dbo.CartItemModels", "CartID", "dbo.CartModels");
            DropIndex("dbo.ProductModels", new[] { "Category_ID" });
            DropIndex("dbo.CartItemModels", new[] { "CartID" });
            DropIndex("dbo.CartItemModels", new[] { "ProductID" });
            DropTable("dbo.CategoryModels");
            DropTable("dbo.ProductModels");
            DropTable("dbo.CartModels");
            DropTable("dbo.CartItemModels");
        }
    }
}
