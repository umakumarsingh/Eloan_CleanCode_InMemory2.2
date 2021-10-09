using E_Loan.Entities;
using Microsoft.EntityFrameworkCore;


namespace E_Loan.DataLayer
{
    public class ELoanDbContext : DbContext
    {
        public ELoanDbContext(DbContextOptions<ELoanDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            //Database.Migrate();
        }
         ///<summary>
        /// Creating DbSet for Table
        /// </summary>
        public DbSet<UserMaster> userMasters { get; set; }
        public DbSet<LoanMaster> loanMasters { get; set; }
        public DbSet<LoanProcesstrans> loanProcesstrans { get; set; }
        public DbSet<LoanApprovaltrans> loanApprovaltrans { get; set; }
        
        /// <summary>
        /// While Model or Table creating Applaying Primary key to Table
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMaster>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<LoanMaster>()
                .HasKey(x => x.LoanId);
            modelBuilder.Entity<LoanProcesstrans>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<LoanApprovaltrans>()
                .HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
