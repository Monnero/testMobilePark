﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace testMobilePark.Migrations
{
    [DbContext(typeof(NewsDb))]
    [Migration("20240515152712_upd_table")]
    partial class upd_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fragment")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FragmentType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Keyword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("VowelCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("News");
                });
#pragma warning restore 612, 618
        }
    }
}
