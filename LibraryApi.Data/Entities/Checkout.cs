using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Data.Entities
{
    public class Checkout
    {
        public int Id { get; set; }

        public virtual Book Book { get; set; }

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