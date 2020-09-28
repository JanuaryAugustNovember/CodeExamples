
using DailyHelperApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyHelperApi.Repository.Context.Mapping
{
    public static class LoanStatusMapping
    {
        public static void MapLoanStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanStatus>(entity =>
            {
                entity.ToTable("loanstatus", "dbo")
                    .HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.LoanId)
                    .HasColumnName("LoanId");
                entity.Property(x => x.StatusCode)
                    .HasColumnName("StatusCode");
                entity.Property(x => x.EffectiveDate)
                    .HasColumnName("EffectiveDate");
                entity.Property(x => x.EntryDate)
                    .HasColumnName("EntryDate");

                entity.HasOne(x => x.Loan)
                    .WithMany(x => x.LoanStatuses)
                    .HasForeignKey(x => x.LoanId);
            });
        }
    }
}
