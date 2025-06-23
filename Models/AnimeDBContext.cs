using System;
using Dotnet_AnimeCRUD.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_AnimeCRUD.Model
{
    // file DataContext ini buat nyambungin ke database
    public class AnimeDBContext : DbContext
    {
        // ketik ctor buat bikin constructor

        // Ini function buat nyambunginnya
        public AnimeDBContext(DbContextOptions<AnimeDBContext> options) : base(options)
        {

        }

        // ketik prop buat bikin property kya gini
        // public int MyProperty { get; set; }
        
        // merepresentasikan tabel Anime yang ada pada db dengan entities pada projek
        public virtual DbSet<Anime> Animes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles{ get; set; }
        public virtual DbSet<AnimeCategory> AnimeCategories { get; set; }


        // Kalau ada relasi many to many di daftarkan disini
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Membuat relasi many to many
            // Menjelaskan bahwa tabel relasi AnimeCategory punya relasi AnimeId dan CategoryId
            modelBuilder.Entity<AnimeCategory>()
                .HasKey(ac => new { ac.AnimeId, ac.CategoryId });

            // Deklarasi one to many dari tabel anime ke tabel relasi AnimeCategories
            // Dengan foreign key atau column di tabel relasinya AnimeId
            modelBuilder.Entity<AnimeCategory>()
                .HasOne(ac => ac.Anime)
                .WithMany(a => a.AnimeCategories)
                .HasForeignKey(ac => ac.AnimeId);

            // Deklarasi one to many dari tabel category ke tabel relasi AnimeCategories
            // Dengan foreign key atau column di tabel relasinya CategoryId
            //modelBuilder.Entity<AnimeCategory>()
            //    .HasOne(ac => ac.Category)
            //    .WithMany(c => c.AnimeCategories)
            //    .HasForeignKey(ac => ac.CategoryId);
        }

    }
}
