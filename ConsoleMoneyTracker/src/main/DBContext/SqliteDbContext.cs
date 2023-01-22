using ConsoleMoneyTracker.src.main.model.dbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.DBContext
{
    public class SqliteDbContext : DbContext
    {
        static string db = "moneytrackerconsole.sqlite";
        public DbSet<AccountDb> accountDbs { get; set; }
        public DbSet<ListItemDb> listItemDbs { get; set; }
        public DbSet<CurrencyDb> currerncyDbs { get; set; }    
        public DbSet<CategoryDb> categoryDbs { get; set; }  
        public DbSet<TransactionDb> transactionDbs { get; set; }    
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite("Data Source="+db);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDb>().ToTable("Account");
            modelBuilder.Entity<TransactionDb>().ToTable("Transaction");
            modelBuilder.Entity<ListItemDb>().ToTable("ListItem")
                .HasOne(l => l.accountDb).WithOne(a => a.item)
                .HasForeignKey<AccountDb>(a => a.listItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ListItemDb>().ToTable("ListItem")
                .HasOne(l => l.currencyDb).WithOne(c => c.item)
                .HasForeignKey<CurrencyDb>(c => c.listItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ListItemDb>().ToTable("ListItem")
                .HasOne(l => l.categoryDb).WithOne(c => c.item)
                .HasForeignKey<CategoryDb>(c => c.listItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CurrencyDb>().ToTable("Currency")
               .HasOne(c => c.accountDb).WithOne(a => a.currency)
               .HasForeignKey<AccountDb>(a => a.currencyId)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CategoryDb>().ToTable("Category");
            modelBuilder.Entity<ListItemDb>().ToTable("ListItem")
                .HasOne(l => l.transactionDb).WithOne(t =>t.item)
                .HasForeignKey<TransactionDb>(t => t.listItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CategoryDb>().ToTable("Category")
                .HasOne(c => c.transactionDb).WithOne(t => t.category)
                .HasForeignKey<TransactionDb>(t => t.categoryId);

            modelBuilder.Entity<AccountDb>().ToTable("Account")
                .HasOne(a => a.transactionDb).WithOne(t => t.account)
                .HasForeignKey<TransactionDb>(t => t.sourceAccountId);

            modelBuilder.Entity<AccountDb>().ToTable("Account")
               .HasOne(a => a.transactionDb).WithOne(t => t.account)
               .HasForeignKey<TransactionDb>(t => t.targetAccountId);

        }
    }
}
