using System;
using LibraryApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using WorkingDaysManagement;

namespace LibraryApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Checkout> Checkouts { get; set; }

        public DbSet<LateCheckIn> LateCheckIns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var wdhelper = new WorkingDayHelper();
            //Books
            modelBuilder.Entity<Book>()
                        .HasData(
                         new Book { Id = 1, Title = "Things Fall Apart", IsAvailable = true, ISBN = "9476028470", PublishYear = "2013", CoverPrice = 20},
                         new Book { Id = 2, Title = "Half of a Yellow Sun", IsAvailable = true, ISBN = "9476028470", PublishYear = "2010", CoverPrice = 12},
                         new Book { Id = 3, Title = "There Was A Country", IsAvailable = true, ISBN = "9476028470", PublishYear = "2018", CoverPrice = 50},
                         new Book { Id = 4, Title = "Worm Hole", IsAvailable = true, ISBN = "9476028470", PublishYear = "2005", CoverPrice = 15},
                         new Book { Id = 5, Title = "Never Man", IsAvailable = true, ISBN = "9476028470", PublishYear = "2000", CoverPrice = 10});

            //Checkout
            modelBuilder.Entity<Checkout>()
                        .HasData(
                            new Checkout { Id = 1, BookId = 3, FullName = "John Doe", Email = "jd@mail.com", PhoneNumber = "08012398765",
                            NationalIdentificationNumber = "9458678475959547",
                            CheckoutDate = wdhelper.PastWorkingDays(DateTime.Now, 15),
                            ExpectedReturnDate = wdhelper.PastWorkingDays(DateTime.Now, 5)}
                        );

        }
    }
}