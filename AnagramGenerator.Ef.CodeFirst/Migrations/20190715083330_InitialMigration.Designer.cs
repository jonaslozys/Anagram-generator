﻿// <auto-generated />
using System;
using AnagramGenerator.Ef.CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramGenerator.Ef.CodeFirst.Migrations
{
    [DbContext(typeof(AnagramContext))]
    [Migration("20190715083330_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.CachedWord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnagramWordId");

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.HasIndex("AnagramWordId");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.UserLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("SearchDate");

                    b.Property<string>("UserIP");

                    b.Property<int?>("WordSearchedId");

                    b.HasKey("Id");

                    b.HasIndex("WordSearchedId");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WordValue")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.CachedWord", b =>
                {
                    b.HasOne("AnagramGenerator.Ef.CodeFirst.Models.Word", "AnagramWord")
                        .WithMany()
                        .HasForeignKey("AnagramWordId");
                });

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.UserLog", b =>
                {
                    b.HasOne("AnagramGenerator.Ef.CodeFirst.Models.Word", "WordSearched")
                        .WithMany()
                        .HasForeignKey("WordSearchedId");
                });
#pragma warning restore 612, 618
        }
    }
}
