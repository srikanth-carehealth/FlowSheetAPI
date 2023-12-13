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
    [Migration("20231213165157_Approvers")]
    partial class Approvers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FlowSheetAPI.DomainModel.Doctor", b =>
                {
                    b.Property<Guid>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("doctor_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("EhrUserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ehr_user_name");

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

                    b.HasKey("DoctorId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.Flowsheet", b =>
                {
                    b.Property<Guid>("FlowsheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("flowsheet_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

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

                    b.Property<Guid>("doctor_id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("flowsheetApprover_id")
                        .HasColumnType("uuid");

                    b.Property<string>("flowsheetNote")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("flowsheet_note");

                    b.Property<Guid>("patient_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("specialityType_id")
                        .HasColumnType("uuid");

                    b.HasKey("FlowsheetId");

                    b.HasIndex("doctor_id");

                    b.HasIndex("flowsheetApprover_id");

                    b.HasIndex("patient_id");

                    b.HasIndex("specialityType_id");

                    b.ToTable("Flowsheet");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetApprovalHistory", b =>
                {
                    b.Property<Guid>("FlowsheetApprovalHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("flowsheetApprovalHistory_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

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

                    b.Property<Guid>("flowsheetApprover_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("flowsheet_id")
                        .HasColumnType("uuid");

                    b.HasKey("FlowsheetApprovalHistoryId");

                    b.HasIndex("flowsheetApprover_id");

                    b.HasIndex("flowsheet_id");

                    b.ToTable("FlowsheetApprovalHistory");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetApprover", b =>
                {
                    b.Property<Guid>("FlowsheetApproverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("flowsheetApprover_id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("Address");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("designation");

                    b.Property<string>("Fax")
                        .HasColumnType("text")
                        .HasColumnName("fax");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("Initial")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("initial");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("row_version");

                    b.Property<string>("Telephone")
                        .HasColumnType("text")
                        .HasColumnName("telephone");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.Property<Guid?>("specialityType_id")
                        .HasColumnType("uuid");

                    b.HasKey("FlowsheetApproverId");

                    b.HasIndex("specialityType_id");

                    b.ToTable("FlowsheetApprover");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetHistory", b =>
                {
                    b.Property<Guid>("FlowsheetHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("flowsheetHistory_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("FlowsheetNote")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("flowsheet_note");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean")
                        .HasColumnName("is_locked");

                    b.Property<string>("LockedBy")
                        .HasColumnType("text")
                        .HasColumnName("locked_by");

                    b.Property<DateTime?>("LockedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("locked_date");

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

                    b.Property<Guid>("doctor_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("flowsheet_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("patient_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("specialityType_id")
                        .HasColumnType("uuid");

                    b.HasKey("FlowsheetHistoryId");

                    b.HasIndex("doctor_id");

                    b.HasIndex("flowsheet_id");

                    b.HasIndex("patient_id");

                    b.HasIndex("specialityType_id");

                    b.ToTable("FlowsheetHistory");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetTemplate", b =>
                {
                    b.Property<Guid>("FlowsheetTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("flowsheetTemplate_id");

                    b.Property<string>("ColumnName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("column_name");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

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

                    b.Property<Guid>("specialityType_id")
                        .HasColumnType("uuid");

                    b.HasKey("FlowsheetTemplateId");

                    b.HasIndex("specialityType_id");

                    b.ToTable("FlowsheetTemplate");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetType", b =>
                {
                    b.Property<Guid>("FlowsheetTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("flowsheetType_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

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

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("FlowsheetTypeId");

                    b.ToTable("FlowsheetType");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.Patient", b =>
                {
                    b.Property<Guid>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("patient_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("EhrUserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ehr_user_name");

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

                    b.HasKey("PatientId");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.SpecialityType", b =>
                {
                    b.Property<Guid>("SpecialityTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("specialityType_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("row_version");

                    b.Property<int>("TotalApprovalCount")
                        .HasColumnType("integer")
                        .HasColumnName("total_approval_count");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("SpecialityTypeId");

                    b.ToTable("SpecialityType");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.Flowsheet", b =>
                {
                    b.HasOne("FlowSheetAPI.DomainModel.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("doctor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowSheetAPI.DomainModel.FlowsheetApprover", "Approver")
                        .WithMany()
                        .HasForeignKey("flowsheetApprover_id");

                    b.HasOne("FlowSheetAPI.DomainModel.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("patient_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowSheetAPI.DomainModel.SpecialityType", "SpecialityType")
                        .WithMany()
                        .HasForeignKey("specialityType_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");

                    b.Navigation("SpecialityType");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetApprovalHistory", b =>
                {
                    b.HasOne("FlowSheetAPI.DomainModel.FlowsheetApprover", "FlowsheetApprover")
                        .WithMany()
                        .HasForeignKey("flowsheetApprover_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowSheetAPI.DomainModel.Flowsheet", "Flowsheet")
                        .WithMany()
                        .HasForeignKey("flowsheet_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flowsheet");

                    b.Navigation("FlowsheetApprover");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetApprover", b =>
                {
                    b.HasOne("FlowSheetAPI.DomainModel.SpecialityType", "SpecialityType")
                        .WithMany()
                        .HasForeignKey("specialityType_id");

                    b.Navigation("SpecialityType");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetHistory", b =>
                {
                    b.HasOne("FlowSheetAPI.DomainModel.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("doctor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowSheetAPI.DomainModel.Flowsheet", "Flowsheet")
                        .WithMany()
                        .HasForeignKey("flowsheet_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowSheetAPI.DomainModel.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("patient_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowSheetAPI.DomainModel.SpecialityType", "SpecialityType")
                        .WithMany()
                        .HasForeignKey("specialityType_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Flowsheet");

                    b.Navigation("Patient");

                    b.Navigation("SpecialityType");
                });

            modelBuilder.Entity("FlowSheetAPI.DomainModel.FlowsheetTemplate", b =>
                {
                    b.HasOne("FlowSheetAPI.DomainModel.SpecialityType", "SpecialityType")
                        .WithMany()
                        .HasForeignKey("specialityType_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpecialityType");
                });
#pragma warning restore 612, 618
        }
    }
}
