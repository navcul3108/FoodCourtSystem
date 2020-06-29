namespace FoodCourtSystem.VendorContextMigrations
{
    using System;
    using System.Data.Entity.Core.Common.CommandTrees;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VendorModels",
                c => new
                {
                    ID = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.VendorModels");
        }
    }
}
