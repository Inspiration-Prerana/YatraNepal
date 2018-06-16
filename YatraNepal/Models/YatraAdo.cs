namespace YatraNepal.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class YatraAdo : DbContext
    {
        // Your context has been configured to use a 'YatraAdo' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'YatraNepal.Models.YatraAdo' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'YatraAdo' 
        // connection string in the application configuration file.
        public YatraAdo()
            : base("name=YatraAdo")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<UserInfo> User { get; set; }
        public virtual DbSet<VehicleInfo> Vehicle { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<AgentInfo> Agent { get; set; }
        public virtual DbSet<AdminInfo> Admin { get; set; }
        public virtual DbSet<VehicleManage> VManage { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}