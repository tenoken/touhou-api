﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TouhouData;
using TouhouData.Context;

namespace TouhouData.Migrations
{
    [DbContext(typeof(TouhouContext))]
    [Migration("20210508175750_ColumnGameplay")]
    partial class ColumnGameplay
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TouhouArticleMaker.Domain.Article", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CardId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Category")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GalleryId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Author", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Card", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhotoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Released")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Gallery", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Photo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GalleryId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GalleryId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Section", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArticleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Article", b =>
                {
                    b.HasOne("TouhouArticleMaker.Domain.Author", null)
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId");

                    b.OwnsOne("TouhouArticleMaker.Domain.Intro", "Intro", b1 =>
                        {
                            b1.Property<string>("ArticleId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(10000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Intro");

                            b1.HasKey("ArticleId");

                            b1.ToTable("Articles");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Title", b1 =>
                        {
                            b1.Property<string>("ArticleId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Title");

                            b1.HasKey("ArticleId");

                            b1.ToTable("Articles");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");
                        });

                    b.Navigation("Intro");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Author", b =>
                {
                    b.OwnsOne("TouhouArticleMaker.Domain.Email", "Email", b1 =>
                        {
                            b1.Property<string>("AuthorId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Address")
                                .HasColumnType("nchar(50)");

                            b1.HasKey("AuthorId");

                            b1.ToTable("Authors");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Name", "Name", b1 =>
                        {
                            b1.Property<string>("AuthorId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("FirstName")
                                .HasColumnType("nchar(50)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnType("nchar(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("AuthorId");

                            b1.ToTable("Authors");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "UserName", b1 =>
                        {
                            b1.Property<string>("AuthorId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AuthorId");

                            b1.ToTable("Authors");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId");
                        });

                    b.Navigation("Email");

                    b.Navigation("Name");

                    b.Navigation("UserName");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Card", b =>
                {
                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Developer", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Developer");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Gameplay", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Gameplay");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Genre", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Genre");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Platforms", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Platforms");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Publisher", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Publisher");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Requirements", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Requirements");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Title", b1 =>
                        {
                            b1.Property<string>("CardId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Title");

                            b1.HasKey("CardId");

                            b1.ToTable("Cards");

                            b1.WithOwner()
                                .HasForeignKey("CardId");
                        });

                    b.Navigation("Developer");

                    b.Navigation("Gameplay");

                    b.Navigation("Genre");

                    b.Navigation("Platforms");

                    b.Navigation("Publisher");

                    b.Navigation("Requirements");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Photo", b =>
                {
                    b.HasOne("TouhouArticleMaker.Domain.Gallery", null)
                        .WithMany("Photos")
                        .HasForeignKey("GalleryId");

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Title", b1 =>
                        {
                            b1.Property<string>("PhotoId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nchar(50)")
                                .HasColumnName("Title");

                            b1.HasKey("PhotoId");

                            b1.ToTable("Photos");

                            b1.WithOwner()
                                .HasForeignKey("PhotoId");
                        });

                    b.Navigation("Title");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Section", b =>
                {
                    b.OwnsOne("TouhouArticleMaker.Domain.Text", "Text", b1 =>
                        {
                            b1.Property<string>("SectionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("TextContent")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SectionId");

                            b1.ToTable("Sections");

                            b1.WithOwner()
                                .HasForeignKey("SectionId");
                        });

                    b.OwnsOne("TouhouArticleMaker.Domain.Title", "Title", b1 =>
                        {
                            b1.Property<string>("SectionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SectionId");

                            b1.ToTable("Sections");

                            b1.WithOwner()
                                .HasForeignKey("SectionId");
                        });

                    b.Navigation("Text");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Author", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("TouhouArticleMaker.Domain.Gallery", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
