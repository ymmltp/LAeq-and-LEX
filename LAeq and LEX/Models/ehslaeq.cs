namespace LAeq_and_LEX.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ehslaeq : DbContext
    {
        public ehslaeq()
            : base("name=ehslaeq")
        {
        }

        public virtual DbSet<setting> settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<setting>()
                .Property(e => e.start_time)
                .IsUnicode(false);

            modelBuilder.Entity<setting>()
                .Property(e => e.end_time)
                .IsUnicode(false);
        }
    }
}
