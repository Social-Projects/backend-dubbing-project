﻿// <auto-generated />
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SoftServe.ITAcademy.BackendDubbingProject.Migrations
{
    [DbContext(typeof(DubbingContext))]
    [Migration("20190115111218_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Dubbing.Models.Audio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.Property<int>("LanguageId");

                    b.Property<int>("SpeechId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("SpeechId");

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("Dubbing.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Dubbing.Models.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("Dubbing.Models.Speech", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PerformanceId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PerformanceId");

                    b.ToTable("Speeches");
                });

            modelBuilder.Entity("Dubbing.Models.Audio", b =>
                {
                    b.HasOne("Dubbing.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dubbing.Models.Speech", "Speech")
                        .WithMany("Audios")
                        .HasForeignKey("SpeechId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dubbing.Models.Speech", b =>
                {
                    b.HasOne("Dubbing.Models.Performance", "Performance")
                        .WithMany("Speeches")
                        .HasForeignKey("PerformanceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
