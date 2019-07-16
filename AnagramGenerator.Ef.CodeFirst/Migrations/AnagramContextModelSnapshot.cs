﻿// <auto-generated />
using System;
using AnagramGenerator.Ef.CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramGenerator.Ef.CodeFirst.Migrations
{
    [DbContext(typeof(AnagramContext))]
    partial class AnagramContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.SearchLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("SearchDate");

                    b.Property<string>("UserIP");

                    b.Property<string>("WordSearched");

                    b.HasKey("Id");

                    b.ToTable("SearchLogs");
                });

            modelBuilder.Entity("AnagramGenerator.Ef.CodeFirst.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableSearches");

                    b.Property<string>("UserIP");

                    b.HasKey("Id");

                    b.ToTable("Users");
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
#pragma warning restore 612, 618
        }
    }
}
