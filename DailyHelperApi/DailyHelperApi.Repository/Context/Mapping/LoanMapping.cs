
using DailyHelperApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyHelperApi.Repository.Context.Mapping
{
    public static class LoanMapping
    {
        public static void MapLoan(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("loan", "dbo")
                    .HasKey(x => x.LoanId);

                entity.Property(x => x.LoanId)
                    .HasColumnName("LoanId")
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.CustomerId)
                    .HasColumnName("CustomerId");

                entity.HasOne<Customer>(c => c.Customer)
                    .WithMany(l => l.Loans)
                    .HasForeignKey(x => x.CustomerId)
                    .IsRequired();
            });
        }
    }
}
