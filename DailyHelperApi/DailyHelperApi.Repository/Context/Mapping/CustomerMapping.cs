
using DailyHelperApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyHelperApi.Repository.Context.Mapping
{
    public static class CustomerMapping
    {
        public static void MapCustomer(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer", "dbo")
                    .HasKey(x => x.CustomerId);

                entity.Property(x => x.CustomerId)
                    .HasColumnName("CustomerId")
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.CreateDate)
                    .HasColumnName("CreateDate");
                entity.Property(x => x.FirstName)
                    .HasColumnName("FirstName");
                entity.Property(x => x.LastName)
                    .HasColumnName("LastName");
                entity.Property(x => x.Email)
                    .HasColumnName("Email");

                entity.HasMany(x => x.Loans)
                    .WithOne(x => x.Customer)
                    .HasForeignKey(x => x.CustomerId);
            });
        }

    }
}
