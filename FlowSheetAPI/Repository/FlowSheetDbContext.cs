using FlowSheetAPI.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace FlowSheetAPI.Repository
{
    public class FlowSheetDbContext : DbContext
    {
        private ILoggerFactory _loggerFactory;

        // Table names
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Flowsheet> Flowsheet { get; set; }
        public DbSet<FlowsheetApprovalHistory> FlowsheetApprovalHistory { get; set; }
        public DbSet<FlowsheetApprover> FlowsheetApprover { get; set; }
        public DbSet<FlowsheetHistory> FlowsheetHistory { get; set; }
        public DbSet<FlowsheetTemplate> FlowsheetTemplate { get; set; }
        public DbSet<LabItem> LabItem { get; set; }
        public DbSet<LabItemSpeciality> LabItemSpeciality { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<SpecialityConditionType> SpecialityConditionType { get; set; }
        public DbSet<SpecialityType> SpecialityType { get; set; }
        
        public FlowSheetDbContext(DbContextOptions<FlowSheetDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }
    }
}