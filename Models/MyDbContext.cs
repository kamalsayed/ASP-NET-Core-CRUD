using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace newProject.Models
{
public partial class MyDbContext : DbContext
{
    public  DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        optionsBuilder.UseSqlServer("Server=localhost;Database=myDB;TrustServerCertificate=true;User=sa;Password=aA@10965856");
    }
}
}