﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motorent.Infrastructure.Common.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Motorent.Infrastructure.Common.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240424112617_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Motorent.Domain.Renters.Renter", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)")
                        .HasColumnName("id");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("character varying(18)")
                        .HasColumnName("cnpj");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_renters");

                    b.HasIndex("Cnpj")
                        .IsUnique()
                        .HasDatabaseName("ix_renters_cnpj");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_renters_user_id");

                    b.ToTable("renters", (string)null);
                });

            modelBuilder.Entity("Motorent.Domain.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)")
                        .HasColumnName("id");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("role");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Motorent.Infrastructure.Common.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Attempt")
                        .HasColumnType("integer")
                        .HasColumnName("attempt");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(8192)
                        .HasColumnType("character varying(8192)")
                        .HasColumnName("data");

                    b.Property<string>("ErrorDetails")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("error_details");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("error_message");

                    b.Property<string>("ErrorType")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("error_type");

                    b.Property<DateTimeOffset?>("NextAttemptAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("next_attempt_at");

                    b.Property<DateTimeOffset?>("ProcessedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_at");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("status");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("Motorent.Infrastructure.Common.Security.RefreshToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(26)
                        .HasColumnType("character varying(26)")
                        .HasColumnName("user_id");

                    b.Property<string>("Token")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("token");

                    b.Property<string>("AccessTokenId")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("access_token_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTimeOffset>("Expires")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expires");

                    b.Property<DateTimeOffset?>("RevokedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("revoked_at");

                    b.Property<DateTimeOffset?>("UsedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("used_at");

                    b.HasKey("UserId", "Token")
                        .HasName("pk_refresh_tokens");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("Motorent.Domain.Renters.Renter", b =>
                {
                    b.OwnsOne("Motorent.Domain.Renters.ValueObjects.Cnh", "Cnh", b1 =>
                        {
                            b1.Property<string>("RenterId")
                                .HasColumnType("character varying(26)")
                                .HasColumnName("id");

                            b1.Property<string>("Category")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("character varying(2)")
                                .HasColumnName("cnh_category");

                            b1.Property<DateOnly>("ExpirationDate")
                                .HasColumnType("date")
                                .HasColumnName("cnh_exp_date");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("cnh_number");

                            b1.HasKey("RenterId");

                            b1.HasIndex("Number")
                                .IsUnique()
                                .HasDatabaseName("ix_renter_cnh_number");

                            b1.ToTable("renters");

                            b1.WithOwner()
                                .HasForeignKey("RenterId")
                                .HasConstraintName("fk_renter_renter_id");
                        });

                    b.Navigation("Cnh")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
