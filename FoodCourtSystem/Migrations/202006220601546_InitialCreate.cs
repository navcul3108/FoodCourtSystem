namespace FoodCourtSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountFundModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        AccountName = c.String(),
                        Balance = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountFundModels");
        }
    }
}
