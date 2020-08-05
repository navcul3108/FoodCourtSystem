namespace FoodCourtSystem.OrderContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        CreatedTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Money = c.Int(nullable: false),
                        Owner = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        Product_ID = c.String(maxLength: 128),
                        OrderModel_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductModels", t => t.Product_ID)
                .ForeignKey("dbo.OrderModels", t => t.OrderModel_ID)
                .Index(t => t.Product_ID)
                .Index(t => t.OrderModel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderModel_ID", "dbo.OrderModels");
            DropForeignKey("dbo.OrderItems", "Product_ID", "dbo.ProductModels");
            DropForeignKey("dbo.ProductModels", "Category_ID", "dbo.CategoryModels");
            DropIndex("dbo.ProductModels", new[] { "Category_ID" });
            DropIndex("dbo.OrderItems", new[] { "OrderModel_ID" });
            DropIndex("dbo.OrderItems", new[] { "Product_ID" });
            DropTable("dbo.CategoryModels");
            DropTable("dbo.ProductModels");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OrderModels");
        }
    }
}
