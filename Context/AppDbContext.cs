using Microsoft.EntityFrameworkCore;
using AlunosApi.Models;
namespace AlunosApi.Context
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        
        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id=1,
                    Nome="Efraim Luis Marcelino",
                    Email="Efraim@alicesoft.pt",
                    Idade=24
                },
                new Aluno
                {
                    Id=2,
                    Nome="Alice mundele",
                    Email="Alice@gmail.com",
                    Idade=60  
                }

                );
        }
    }
}
