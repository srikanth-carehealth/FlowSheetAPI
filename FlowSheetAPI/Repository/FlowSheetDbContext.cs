using FlowSheetAPI.DomainModel.Endocrinology;
using Microsoft.EntityFrameworkCore;

namespace FlowSheetAPI.Repository
{
    public class FlowSheetDbContext : DbContext
    {
        
        // Table names
        public DbSet<Endocrinology> Endocrinology { get; set; }

        public FlowSheetDbContext(DbContextOptions<FlowSheetDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
