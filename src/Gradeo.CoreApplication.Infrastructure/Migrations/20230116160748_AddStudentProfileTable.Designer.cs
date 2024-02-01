﻿// <auto-generated />
using System;
using Gradeo.CoreApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gradeo.CoreApplication.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230116160748_AddStudentProfileTable")]
    partial class AddStudentProfileTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.AssignedSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessUnitId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MasterSubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUnitId");

                    b.HasIndex("MasterSubjectId");

                    b.ToTable("AssignedSubjects");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.BusinessUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BusinessUnitType")
                        .HasColumnType("int");

                    b.Property<string>("CosmosDbConnection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("BusinessUnits");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.MasterSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BusinessUnitId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUnitId");

                    b.ToTable("MasterSubjects");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.PermissionDescriptor", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionId");

                    b.ToTable("PermissionDescriptors");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BusinessUnitId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUnitId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.SchoolInfo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SchoolsInfo");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.StudentProfile", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("StudentProfiles");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BusinessUnitId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUnitId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("LastModifiedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.AssignedSubject", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.BusinessUnit", "BusinessUnit")
                        .WithMany()
                        .HasForeignKey("BusinessUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.MasterSubject", "MasterSubject")
                        .WithMany()
                        .HasForeignKey("MasterSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessUnit");

                    b.Navigation("MasterSubject");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.MasterSubject", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.BusinessUnit", "BusinessUnit")
                        .WithMany()
                        .HasForeignKey("BusinessUnitId");

                    b.Navigation("BusinessUnit");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.Role", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.BusinessUnit", "BusinessUnit")
                        .WithMany()
                        .HasForeignKey("BusinessUnitId");

                    b.Navigation("BusinessUnit");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.PermissionDescriptor", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.SchoolInfo", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.BusinessUnit", "BusinessUnit")
                        .WithOne()
                        .HasForeignKey("Gradeo.CoreApplication.Domain.Entities.SchoolInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessUnit");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.StudentProfile", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Gradeo.CoreApplication.Domain.Entities.StudentProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.User", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.BusinessUnit", "BusinessUnit")
                        .WithMany()
                        .HasForeignKey("BusinessUnitId");

                    b.Navigation("BusinessUnit");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Gradeo.CoreApplication.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.Role", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Gradeo.CoreApplication.Domain.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
