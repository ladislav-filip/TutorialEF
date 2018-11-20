﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutorTerm.DAL;

namespace TutorTerm.Migrations
{
    [DbContext(typeof(TutorContext))]
    partial class TutorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("TutorTerm.DAL.Model.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("UserId");

                    b.HasKey("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Car");

                    b.HasData(
                        new { CarId = 1, Name = "NJB 45-28", UserId = 1 },
                        new { CarId = 2, Name = "FMC 22-22", UserId = 1 },
                        new { CarId = 3, Name = "1T5 00-01", UserId = 3 }
                    );
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Alpha")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("HexValue")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<string>("Name");

                    b.HasKey("ColorId");

                    b.ToTable("Color");

                    b.HasData(
                        new { ColorId = 1, Alpha = true, HexValue = "#0000FF", Name = "Blue" },
                        new { ColorId = 2, Alpha = false, HexValue = "#FF0000", Name = "Red" }
                    );
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Gender", b =>
                {
                    b.Property<byte>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Gender");

                    b.HasData(
                        new { Id = (byte)1, Name = "male" },
                        new { Id = (byte)2, Name = "female" }
                    );
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Language", b =>
                {
                    b.Property<short>("LanguageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("LanguageId");

                    b.ToTable("Language");

                    b.HasData(
                        new { LanguageId = (short)1, Code = "cs", Name = "czech" },
                        new { LanguageId = (short)2, Code = "pl", Name = "polski" },
                        new { LanguageId = (short)3, Code = "ru", Name = "русский" },
                        new { LanguageId = (short)4, Code = "en", Name = "english" }
                    );
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Order", b =>
                {
                    b.Property<string>("Prefix");

                    b.Property<int>("Year");

                    b.Property<int>("Number");

                    b.Property<string>("Note");

                    b.HasKey("Prefix", "Year", "Number");

                    b.ToTable("MyOrders");
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.OrderItem", b =>
                {
                    b.Property<int>("ItemNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<int>("Number");

                    b.Property<string>("Prefix");

                    b.Property<decimal>("Price");

                    b.Property<int>("Year");

                    b.HasKey("ItemNumber");

                    b.HasIndex("Prefix", "Year", "Number");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Active")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<short>("Age");

                    b.Property<byte>("GenderId");

                    b.Property<short>("LanguageId");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.Property<string>("UsrType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasMaxLength(20);

                    b.HasKey("UserId");

                    b.HasIndex("GenderId");

                    b.HasIndex("LanguageId");

                    b.ToTable("User");

                    b.HasData(
                        new { UserId = 1, Active = "T", Age = (short)0, GenderId = (byte)1, LanguageId = (short)3, Name = "Petr", Surname = "Veliký", UsrType = "Unknown" },
                        new { UserId = 2, Active = "T", Age = (short)0, GenderId = (byte)2, LanguageId = (short)4, Name = "Alžběta", Surname = "Druhá", UsrType = "Unknown" },
                        new { UserId = 3, Active = "T", Age = (short)0, GenderId = (byte)1, LanguageId = (short)1, Name = "Karel", Surname = "Čtrvtý", UsrType = "Unknown" }
                    );
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Car", b =>
                {
                    b.HasOne("TutorTerm.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.Customer", b =>
                {
                    b.OwnsOne("TutorTerm.DAL.Model.Address", "Address", b1 =>
                        {
                            b1.Property<int>("CustomerId");

                            b1.Property<int>("AddressId");

                            b1.Property<string>("City");

                            b1.Property<string>("Street");

                            b1.Property<string>("Zip");

                            b1.ToTable("Customer");

                            b1.HasOne("TutorTerm.DAL.Model.Customer")
                                .WithOne("Address")
                                .HasForeignKey("TutorTerm.DAL.Model.Address", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.OrderItem", b =>
                {
                    b.HasOne("TutorTerm.DAL.Model.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("Prefix", "Year", "Number")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TutorTerm.DAL.Model.User", b =>
                {
                    b.HasOne("TutorTerm.DAL.Model.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TutorTerm.DAL.Model.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("TutorTerm.DAL.Model.Address", "Address", b1 =>
                        {
                            b1.Property<int>("UserId");

                            b1.Property<int>("AddressId");

                            b1.Property<string>("City");

                            b1.Property<string>("Street");

                            b1.Property<string>("Zip");

                            b1.ToTable("Address");

                            b1.HasOne("TutorTerm.DAL.Model.User")
                                .WithOne("Address")
                                .HasForeignKey("TutorTerm.DAL.Model.Address", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
