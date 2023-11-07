﻿// <auto-generated />
using System;
using FlowSheetAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlowSheetAPI.Migrations
{
    [DbContext(typeof(FlowSheetDbContext))]
    [Migration("20231107215612_EndocrinologyLockColumns")]
    partial class EndocrinologyLockColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlowSheetAPI.DomainModel.Endocrinology.Endocrinology", b =>
                {
                    b.Property<Guid>("EndocrinologyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float?>("A1C")
                        .HasColumnType("real");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Dose")
                        .HasColumnType("text");

                    b.Property<DateTime?>("InitialDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initial_date");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_locked");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text")
                        .HasColumnName("locked_by");

                    b.Property<DateTime?>("LockedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("locked_date");

                    b.Property<string>("Medication")
                        .HasColumnType("text");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid")
                        .HasColumnName("patient_id");

                    b.Property<string>("Recommendation")
                        .HasColumnType("text");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("row_version");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("EndocrinologyId");

                    b.ToTable("Endocrinology");
                });
#pragma warning restore 612, 618
        }
    }
}
