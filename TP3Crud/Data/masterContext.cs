using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TP3Crud.Models;

namespace TP3Crud.Data
{
    public partial class masterContext : DbContext
    {
        public masterContext()
        {
        }

        public masterContext(DbContextOptions<masterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Encarregado> Encarregado { get; set; } = null!;
        public virtual DbSet<Especializacao> Especializacao { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionario { get; set; } = null!;
        public virtual DbSet<Tarefa> Tarefa { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=master;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encarregado>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Encarreg__A9D1053467610E25")
                    .IsUnique();

                entity.Property(e => e.EncarregadoId).HasColumnName("EncarregadoID");

                entity.Property(e => e.DataContratacao).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nome).HasMaxLength(255);
            });

            modelBuilder.Entity<Especializacao>(entity =>
            {
                entity.Property(e => e.EspecializacaoId).HasColumnName("EspecializacaoID");

                entity.Property(e => e.EncarregadoId).HasColumnName("EncarregadoID");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.HasOne(d => d.Encarregado)
                    .WithMany(p => p.Especializacao)
                    .HasForeignKey(d => d.EncarregadoId)
                    .HasConstraintName("FK__Especiali__Encar__2B5F6B28");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Funciona__A9D105344D44D669")
                    .IsUnique();

                entity.Property(e => e.FuncionarioId).HasColumnName("FuncionarioID");

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nome).HasMaxLength(255);
            });

            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.Property(e => e.TarefaId).HasColumnName("TarefaID");

                entity.Property(e => e.DataFim).HasColumnType("date");

                entity.Property(e => e.DataInicio).HasColumnType("date");

                entity.Property(e => e.EspecializacaoId).HasColumnName("EspecializacaoID");

                entity.Property(e => e.FuncionarioId).HasColumnName("FuncionarioID");

                entity.Property(e => e.Nome).HasMaxLength(255);

                entity.HasOne(d => d.Especializacao)
                    .WithMany(p => p.Tarefa)
                    .HasForeignKey(d => d.EspecializacaoId)
                    .HasConstraintName("FK__Tarefa__Especial__2F2FFC0C");

                entity.HasOne(d => d.Funcionario)
                    .WithMany(p => p.Tarefa)
                    .HasForeignKey(d => d.FuncionarioId)
                    .HasConstraintName("FK__Tarefa__Funciona__2E3BD7D3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
