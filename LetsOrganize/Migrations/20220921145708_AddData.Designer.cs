// <auto-generated />
using System;
using LetsOrganize.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LetsOrganize.Migrations
{
    [DbContext(typeof(LetsOrganizeDbContext))]
    [Migration("20220921145708_AddData")]
    partial class AddData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LetsOrganize.Entities.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("MyListId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MyListId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("LetsOrganize.Entities.MyList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Lists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ShoppingList"
                        });
                });

            modelBuilder.Entity("LetsOrganize.Entities.Element", b =>
                {
                    b.HasOne("LetsOrganize.Entities.MyList", "MyList")
                        .WithMany("ElementsList")
                        .HasForeignKey("MyListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MyList");
                });

            modelBuilder.Entity("LetsOrganize.Entities.MyList", b =>
                {
                    b.Navigation("ElementsList");
                });
#pragma warning restore 612, 618
        }
    }
}
