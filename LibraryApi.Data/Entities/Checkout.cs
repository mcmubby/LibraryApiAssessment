using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Data.Entities
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }

        public virtual Book Book { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public string NationalIdentificationNumber { get; set; }

        //public virtual User User { get; set; }

        //public int UserId { get; set; }

        public DateTime CheckoutDate { get; set; }

        public DateTime ExpectedReturnDate { get; set; }
    }
}