using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DailyHelperApi.Domain.Entities
{
    //[Table("customer")]
    public class Customer
    {
        //[Key]
        //[Column("CustomerId")]
        public decimal CustomerId { get; set; }

        //[Column("Date")]
        public DateTime CreateDate { get; set; }

        //[Column("FirstName")]
        public string FirstName { get; set; }

        //[Column("LastName")]
        public string LastName { get; set; }

        //[Column("Email")]
        public string Email { get; set; }

        //[ForeignKey("CustomerNumber")]
        public Collection<Loan> Loans { get; set; }
    }
}
