namespace FitnessLog.WebApi.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Logs",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 4000),
                        Username = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.LogID);
            
            CreateTable(
                "LogEntries",
                c => new
                    {
                        LogEntryID = c.Int(nullable: false, identity: true),
                        DateAndTime = c.String(maxLength: 4000),
                        ExerciseName = c.String(maxLength: 4000),
                        Lbs = c.Int(nullable: false),
                        Reps = c.Int(nullable: false),
                        Set = c.Int(nullable: false),
                        Log_LogID = c.Int(),
                    })
                .PrimaryKey(t => t.LogEntryID)
                .ForeignKey("Logs", t => t.Log_LogID)
                .Index(t => t.Log_LogID);
            
        }
        
        public override void Down()
        {
            DropIndex("LogEntries", new[] { "Log_LogID" });
            DropForeignKey("LogEntries", "Log_LogID", "Logs");
            DropTable("LogEntries");
            DropTable("Logs");
        }
    }
}
