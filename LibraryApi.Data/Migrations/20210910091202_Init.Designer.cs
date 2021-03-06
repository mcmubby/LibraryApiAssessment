// <auto-generated />
using System;
using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210910091202_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryApi.Data.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CoverPrice")
                        .HasColumnType("Money");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("PublishYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoverPrice = 20m,
                            ISBN = "9476028470",
                            IsAvailable = true,
                            PublishYear = "2013",
                            Title = "Things Fall Apart"
                        },
                        new
                        {
                            Id = 2,
                            CoverPrice = 12m,
                            ISBN = "9476028470",
                            IsAvailable = true,
                            PublishYear = "2010",
                            Title = "Half of a Yellow Sun"
                        },
                        new
                        {
                            Id = 3,
                            CoverPrice = 50m,
                            ISBN = "9476028470",
                            IsAvailable = true,
                            PublishYear = "2018",
                            Title = "There Was A Country"
                        },
                        new
                        {
                            Id = 4,
                            CoverPrice = 15m,
                            ISBN = "9476028470",
                            IsAvailable = true,
                            PublishYear = "2005",
                            Title = "Worm Hole"
                        },
                        new
                        {
                            Id = 5,
                            CoverPrice = 10m,
                            ISBN = "9476028470",
                            IsAvailable = true,
                            PublishYear = "2000",
                            Title = "Never Man"
                        });
                });

            modelBuilder.Entity("LibraryApi.Data.Entities.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpectedReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalIdentificationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Checkouts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 3,
                            CheckoutDate = new DateTime(2021, 8, 20, 10, 12, 1, 324, DateTimeKind.Local).AddTicks(8875),
                            Email = "jd@mail.com",
                            ExpectedReturnDate = new DateTime(2021, 9, 3, 10, 12, 1, 333, DateTimeKind.Local).AddTicks(5018),
                            FullName = "John Doe",
                            NationalIdentificationNumber = "9458678475959547",
                            PhoneNumber = "08012398765"
                        });
                });

            modelBuilder.Entity("LibraryApi.Data.Entities.LateCheckIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CheckoutId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectedReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfDaysLate")
                        .HasColumnType("int");

                    b.Property<decimal>("PenaltyFees")
                        .HasColumnType("Money");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CheckoutId");

                    b.ToTable("LateCheckIns");
                });

            modelBuilder.Entity("LibraryApi.Data.Entities.Checkout", b =>
                {
                    b.HasOne("LibraryApi.Data.Entities.Book", "Book")
                        .WithMany("CheckoutHistory")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryApi.Data.Entities.LateCheckIn", b =>
                {
                    b.HasOne("LibraryApi.Data.Entities.Checkout", "Checkout")
                        .WithMany()
                        .HasForeignKey("CheckoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Checkout");
                });

            modelBuilder.Entity("LibraryApi.Data.Entities.Book", b =>
                {
                    b.Navigation("CheckoutHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
