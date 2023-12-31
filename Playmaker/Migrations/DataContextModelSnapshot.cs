﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Playmaker.Data;

#nullable disable

namespace Playmaker.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Playmaker.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldId")
                        .HasColumnType("int")
                        .HasColumnName("field_id");

                    b.Property<DateTime>("PlayDateEnd")
                        .HasColumnType("datetime2")
                        .HasColumnName("play_date_end");

                    b.Property<DateTime>("PlayDateStart")
                        .HasColumnType("datetime2")
                        .HasColumnName("play_date_start");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("UserId");

                    b.ToTable("bookings", (string)null);
                });

            modelBuilder.Entity("Playmaker.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.Property<int>("VenueId")
                        .HasColumnType("int")
                        .HasColumnName("venue_id");

                    b.HasKey("Id");

                    b.HasIndex("VenueId");

                    b.ToTable("fields", (string)null);
                });

            modelBuilder.Entity("Playmaker.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Playmaker.Models.UserBooking", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("BookingId")
                        .HasColumnType("int")
                        .HasColumnName("booking_id");

                    b.HasKey("UserId", "BookingId");

                    b.HasIndex("BookingId");

                    b.ToTable("user_has_booking", (string)null);
                });

            modelBuilder.Entity("Playmaker.Models.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("venues", (string)null);
                });

            modelBuilder.Entity("Playmaker.Models.Booking", b =>
                {
                    b.HasOne("Playmaker.Models.Field", "Field")
                        .WithMany("Bookings")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Playmaker.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Field");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Playmaker.Models.Field", b =>
                {
                    b.HasOne("Playmaker.Models.Venue", "Venue")
                        .WithMany("Fields")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("Playmaker.Models.UserBooking", b =>
                {
                    b.HasOne("Playmaker.Models.Booking", "Booking")
                        .WithMany("UserBookings")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Playmaker.Models.User", "User")
                        .WithMany("UserBookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Playmaker.Models.Venue", b =>
                {
                    b.HasOne("Playmaker.Models.User", "User")
                        .WithMany("Venues")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Playmaker.Models.Booking", b =>
                {
                    b.Navigation("UserBookings");
                });

            modelBuilder.Entity("Playmaker.Models.Field", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Playmaker.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("UserBookings");

                    b.Navigation("Venues");
                });

            modelBuilder.Entity("Playmaker.Models.Venue", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}
