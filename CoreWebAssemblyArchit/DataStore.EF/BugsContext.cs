using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataStore.EF
{
    public class BugsContext : DbContext
    {
        //BugsContext is the database/schema in memory- ORM loads the data in memory
        public BugsContext(DbContextOptions options) : base(options) {}

        //need to map between data in memory and the tables in db
        //members of the table, each dbSet corresponds to a table
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        //create relationships between the tables- create schema here
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Project>()
                //EF knows to link Ids
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                //in-memory db doesn't have relational db
                .HasForeignKey(t => t.ProjectId);

            //seed data, when seeding data, the auto increment id is turned off
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Name = "Project 1"},
                new Project { ProjectId = 2, Name = "Project 2" }
                );

            modelBuilder.Entity<Ticket>().HasData(
             new Ticket { TicketId = 1, Title = "Bug #1", ProjectId = 1, Owner = "Dee", ReportDate = new DateTime(2021, 1, 1), DueDate = new DateTime(2021, 2, 1) },
             new Ticket { TicketId = 2, Title = "Bug #2", ProjectId = 1, Owner = "Sheila", ReportDate = new DateTime(2021, 3, 1), DueDate = new DateTime(2021, 12, 1) },
             new Ticket { TicketId = 3, Title = "Bug #3", ProjectId = 2, Owner = "Elsa", ReportDate = new DateTime(2021, 1, 31), DueDate = new DateTime(2021, 12, 15) }
             );

            //During development we are using in-memory database- install EF InMemory and wire it in Startup.cs
        }

    }
}
