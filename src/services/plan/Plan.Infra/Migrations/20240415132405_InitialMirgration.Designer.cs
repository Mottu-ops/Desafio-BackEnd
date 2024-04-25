﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plan.Infra.Context;

#nullable disable

namespace Plan.Infrastructure.Migrations
{
    [DbContext(typeof(PlanContext))]
    [Migration("20240415132405_InitialMirgration")]
    partial class InitialMirgration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Plan.Domain.Entity.RentPlan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Duration")
                        .HasMaxLength(80)
                        .HasColumnType("INTEGER")
                        .HasColumnName("plateCode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("name");

                    b.Property<string>("Tax")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("tax");

                    b.HasKey("Id");

                    b.ToTable("plans", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
