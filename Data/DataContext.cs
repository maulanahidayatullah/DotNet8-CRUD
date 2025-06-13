using System;
using Dotnet_AnimeCRUD.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_AnimeCRUD.Data
{
    // file DataContext ini buat nyambungin ke database
    public class DataContext : DbContext
    {
        // ketik ctor buat bikin constructor

        // Ini function buat nyambunginnya
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        // ketik prop buat bikin property kya gini
        // public int MyProperty { get; set; }
        
        // merepresentasikan tabel Anime yang ada pada db dengan entities pada projek
        public DbSet<Anime> Animes { get; set; }

    }
}
