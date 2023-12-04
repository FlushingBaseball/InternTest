﻿// <auto-generated />
using System;
using ContactManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactManagerStarter.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactManager.Data.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Zip")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b39467d9-a1f4-4843-aee9-7e9060448ded"),
                            City = "Melvile",
                            ContactId = new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"),
                            State = "NY",
                            Street1 = "10 Main St",
                            Street2 = "",
                            Type = 0,
                            Zip = 11757
                        },
                        new
                        {
                            Id = new Guid("1632295e-fc3c-49c5-b30f-d673fc816b73"),
                            City = "Westbury",
                            ContactId = new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                            State = "NY",
                            Street1 = "245 Coral Place",
                            Street2 = "Appt #3",
                            Type = 0,
                            Zip = 11590
                        },
                        new
                        {
                            Id = new Guid("a0762d8b-9dc6-4fa0-99dc-6f7438732760"),
                            City = "Los Angles",
                            ContactId = new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                            State = "CA",
                            Street1 = "1 Apple Way",
                            Street2 = "",
                            Type = 1,
                            Zip = 11757
                        },
                        new
                        {
                            Id = new Guid("7931505c-fca8-4a35-8d24-b32df341643d"),
                            City = "Melvile",
                            ContactId = new Guid("99580d68-9d2f-4552-862e-06b3204193f1"),
                            State = "NY",
                            Street1 = "10 Main St",
                            Street2 = "",
                            Type = 0,
                            Zip = 11757
                        });
                });

            modelBuilder.Entity("ContactManager.Data.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"),
                            DOB = new DateTime(1960, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Bill",
                            LastName = "Gates",
                            Title = "Mr."
                        },
                        new
                        {
                            Id = new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                            DOB = new DateTime(1950, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Steve",
                            LastName = "Jobs",
                            Title = "Mr."
                        },
                        new
                        {
                            Id = new Guid("99580d68-9d2f-4552-862e-06b3204193f1"),
                            DOB = new DateTime(1980, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Sundar",
                            LastName = "Pichai",
                            Title = "Mr."
                        });
                });

            modelBuilder.Entity("ContactManager.Data.EmailAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrimaryDisplay")
                        .HasColumnType("bit");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("EmailAddresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5111f412-a7f4-4169-bb27-632687569ccd"),
                            ContactId = new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"),
                            Email = "Bill@gates.com",
                            IsPrimaryDisplay = false,
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("3ddeb084-5e5d-4eca-b275-e4f6886e04dc"),
                            ContactId = new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                            Email = "Steve@Jobs.com",
                            IsPrimaryDisplay = false,
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("3a406f64-ad7b-4098-ab01-7e93aae2b851"),
                            ContactId = new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                            Email = "SteveJobs@apple.com",
                            IsPrimaryDisplay = false,
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("d1a50413-20c0-4972-a351-8be24e1fc939"),
                            ContactId = new Guid("99580d68-9d2f-4552-862e-06b3204193f1"),
                            Email = "SundarPichai@gmail.com",
                            IsPrimaryDisplay = false,
                            Type = 1
                        });
                });

            modelBuilder.Entity("ContactManager.Data.Address", b =>
                {
                    b.HasOne("ContactManager.Data.Contact", "Contact")
                        .WithMany("Addresses")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ContactManager.Data.EmailAddress", b =>
                {
                    b.HasOne("ContactManager.Data.Contact", "Contact")
                        .WithMany("EmailAddresses")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ContactManager.Data.Contact", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("EmailAddresses");
                });
#pragma warning restore 612, 618
        }
    }
}
