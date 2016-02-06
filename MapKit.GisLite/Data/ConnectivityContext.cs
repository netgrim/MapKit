using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace MapKit.GisLite
{
    class ConnectivityContext : DbContext
    {
        public DbSet<FeatureType> FeatureTypes { get; set; }

        public ConnectivityContext()
        {
        }

        public ConnectivityContext(SQLiteConnection connection)
            : base(connection, false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var devIntConn = modelBuilder.Entity<FeatureType>();
            devIntConn.ToTable("ent_def");
            devIntConn.Property(t => t.DefType).HasColumnName("def_type");
            devIntConn.Property(t => t.TableName).HasColumnName("table_name");
        }
     
         
    }

    
}
