﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp1.Data;

namespace WebApp1.Migrations
{
    [DbContext(typeof(WebApp1Context))]
    [Migration("20230425045357_4thmigration")]
    partial class _4thmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApp1.Data.Model.Batch", b =>
                {
                    b.Property<string>("BatchGuid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BusinessUnitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BatchGuid");

                    b.ToTable("Batch");
                });

            modelBuilder.Entity("WebApp1.Data.Model.BatchAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BatchGuid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BatchGuid");

                    b.ToTable("BatchAttributes");
                });

            modelBuilder.Entity("WebApp1.Data.Model.BatchFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BatchGuid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("filepath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BatchGuid");

                    b.ToTable("BatchFiles");
                });

            modelBuilder.Entity("WebApp1.Data.Model.Groups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BatchGuid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BatchGuid");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("WebApp1.Data.Model.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("WebApp1.Data.Model.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BatchGuid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BatchGuid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApp1.Data.Model.BatchAttribute", b =>
                {
                    b.HasOne("WebApp1.Data.Model.Batch", null)
                        .WithMany("_batchattributes")
                        .HasForeignKey("BatchGuid");
                });

            modelBuilder.Entity("WebApp1.Data.Model.BatchFiles", b =>
                {
                    b.HasOne("WebApp1.Data.Model.Batch", null)
                        .WithMany("BatchFiles")
                        .HasForeignKey("BatchGuid");
                });

            modelBuilder.Entity("WebApp1.Data.Model.Groups", b =>
                {
                    b.HasOne("WebApp1.Data.Model.Batch", null)
                        .WithMany("_groups")
                        .HasForeignKey("BatchGuid");
                });

            modelBuilder.Entity("WebApp1.Data.Model.Users", b =>
                {
                    b.HasOne("WebApp1.Data.Model.Batch", null)
                        .WithMany("_users")
                        .HasForeignKey("BatchGuid");
                });

            modelBuilder.Entity("WebApp1.Data.Model.Batch", b =>
                {
                    b.Navigation("_batchattributes");

                    b.Navigation("_groups");

                    b.Navigation("_users");

                    b.Navigation("BatchFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
