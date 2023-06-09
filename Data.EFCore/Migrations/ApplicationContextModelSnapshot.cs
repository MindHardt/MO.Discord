﻿// <auto-generated />
using System;
using Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.EFCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Entities.GuildData", b =>
                {
                    b.Property<ulong>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<bool>("AdultAllowed")
                        .HasColumnType("boolean");

                    b.Property<string>("GuildName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("InlineTagsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("InlineTagsPrefix")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("$");

                    b.HasKey("GuildId");

                    b.ToTable("GuildData");
                });

            modelBuilder.Entity("Data.Entities.Tags.Tag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<ulong?>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<ulong>("OwnerId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("GuildId", "Name")
                        .IsUnique();

                    b.ToTable("Tags", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Data.Entities.Users.UserData", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("integer");

                    b.Property<int?>("CustomTagLimit")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("UserData");
                });

            modelBuilder.Entity("Data.Entities.Tags.AliasTag", b =>
                {
                    b.HasBaseType("Data.Entities.Tags.Tag");

                    b.Property<long>("ReferencedTagId")
                        .HasColumnType("bigint");

                    b.HasIndex("ReferencedTagId");

                    b.HasDiscriminator().HasValue("AliasTag");
                });

            modelBuilder.Entity("Data.Entities.Tags.MessageTag", b =>
                {
                    b.HasBaseType("Data.Entities.Tags.Tag");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.HasDiscriminator().HasValue("MessageTag");
                });

            modelBuilder.Entity("Data.Entities.Tags.Tag", b =>
                {
                    b.HasOne("Data.Entities.GuildData", "Guild")
                        .WithMany("Tags")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.Users.UserData", "Owner")
                        .WithMany("Tags")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guild");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Data.Entities.Tags.AliasTag", b =>
                {
                    b.HasOne("Data.Entities.Tags.Tag", "ReferencedTag")
                        .WithMany("Aliases")
                        .HasForeignKey("ReferencedTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReferencedTag");
                });

            modelBuilder.Entity("Data.Entities.GuildData", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Data.Entities.Tags.Tag", b =>
                {
                    b.Navigation("Aliases");
                });

            modelBuilder.Entity("Data.Entities.Users.UserData", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
