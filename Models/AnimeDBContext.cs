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

    }
}
