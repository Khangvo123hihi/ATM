
using Microsoft.EntityFrameworkCore;

namespace ATM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 14 bảng trong ATM_DB
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AtmCard> ATMCards { get; set; }
        public DbSet<CardPinAttempt> CardPINAttempts { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<TransactionCancellation> TransactionCancellations { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<TransferTransaction> TransferTransactions { get; set; }
        public DbSet<BankIntegrationLog> BankIntegrationLogs { get; set; }
        public DbSet<VisaCard> VisaCards { get; set; }
        public DbSet<VisaTransactionLog> VisaTransactionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================================
            // 1. CUSTOMER
            // =====================================
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.HasIndex(x => x.CustomerCode)
                    .IsUnique();

                entity.HasIndex(x => x.IdentityNumber)
                    .IsUnique();
            });

            // =====================================
            // 2. ACCOUNT
            // =====================================
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");

                entity.HasIndex(x => x.AccountNumber)
                    .IsUnique();

                entity.Property(x => x.Balance)
                    .HasPrecision(18, 2);

                entity.HasOne(x => x.Customer)
                    .WithMany(x => x.Accounts)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 3. ATM CARD
            // =====================================
            modelBuilder.Entity<AtmCard>(entity =>
            {
                entity.ToTable("ATMCards");

                entity.HasIndex(x => x.CardNumberHash)
                    .IsUnique();

                entity.Property(x => x.CardNumberHash)
                    .IsUnicode(false);

                entity.HasOne(x => x.Account)
                    .WithMany(x => x.ATMCards)
                    .HasForeignKey(x => x.AccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 4. CARD PIN ATTEMPT
            // =====================================
            modelBuilder.Entity<CardPinAttempt>(entity =>
            {
                entity.ToTable("CardPINAttempts");

                entity.HasOne(x => x.AtmCard)
                    .WithMany(x => x.CardPinAttempts)
                    .HasForeignKey(x => x.CardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // =====================================
            // 5. BANK
            // =====================================
            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Banks");

                entity.HasIndex(x => x.BankCode)
                    .IsUnique();

                entity.Property(x => x.BankCode)
                    .IsUnicode(false);
            });

            // =====================================
            // 6. BANK ACCOUNT
            // =====================================
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.ToTable("BankAccounts");

                entity.HasIndex(x => new
                {
                    x.BankId,
                    x.AccountNumberHash
                })
                .IsUnique();

                entity.HasOne(x => x.Bank)
                    .WithMany(x => x.BankAccounts)
                    .HasForeignKey(x => x.BankId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 7. TRANSACTION
            // =====================================
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions");

                entity.HasIndex(x => x.TransactionCode)
                    .IsUnique();

                entity.Property(x => x.Amount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.BalanceBefore)
                    .HasPrecision(18, 2);

                entity.Property(x => x.BalanceAfter)
                    .HasPrecision(18, 2);

                entity.HasOne(x => x.Account)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.AccountId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.AtmCard)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.CardId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 8. TRANSACTION DETAIL
            // =====================================
            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.ToTable("TransactionDetails");

                entity.HasIndex(x => x.TransactionId)
                    .IsUnique();

                entity.Property(x => x.Fee)
                    .HasPrecision(18, 2);

                entity.Property(x => x.ExchangeRate)
                    .HasPrecision(18, 6);

                entity.HasOne(x => x.Transaction)
                    .WithOne(x => x.TransactionDetail)
                    .HasForeignKey<TransactionDetail>(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 9. TRANSACTION CANCELLATION
            // =====================================
            modelBuilder.Entity<TransactionCancellation>(entity =>
            {
                entity.ToTable("TransactionCancellations");

                entity.HasIndex(x => x.TransactionId)
                    .IsUnique();

                entity.Property(x => x.RefundAmount)
                    .HasPrecision(18, 2);

                entity.HasOne(x => x.Transaction)
                    .WithOne()
                    .HasForeignKey<TransactionCancellation>(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 10. RECEIPT
            // =====================================
            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipts");

                entity.HasIndex(x => x.ReceiptNumber)
                    .IsUnique();

                entity.HasIndex(x => x.TransactionId)
                    .IsUnique();

                entity.HasOne(x => x.Transaction)
                    .WithOne()
                    .HasForeignKey<Receipt>(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 11. TRANSFER TRANSACTION
            // =====================================
            modelBuilder.Entity<TransferTransaction>(entity =>
            {
                entity.ToTable("TransferTransactions");

                entity.Property(x => x.TransferAmount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.TransferFee)
                    .HasPrecision(18, 2);

                entity.HasOne(x => x.Transaction)
                    .WithOne()
                    .HasForeignKey<TransferTransaction>(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.SourceAccount)
                    .WithMany(x => x.OutgoingTransfers)
                    .HasForeignKey(x => x.SourceAccountId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.DestinationBank)
                    .WithMany()
                    .HasForeignKey(x => x.DestinationBankId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.DestinationBankAccount)
                    .WithMany()
                    .HasForeignKey(x => x.DestinationBankAccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 12. BANK INTEGRATION LOG
            // =====================================
            modelBuilder.Entity<BankIntegrationLog>(entity =>
            {
                entity.ToTable("BankIntegrationLogs");

                entity.HasOne(x => x.Bank)
                    .WithMany(x => x.BankIntegrationLogs)
                    .HasForeignKey(x => x.BankId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Transaction)
                    .WithMany()
                    .HasForeignKey(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 13. VISA CARD
            // =====================================
            modelBuilder.Entity<VisaCard>(entity =>
            {
                entity.ToTable("VisaCards");

                entity.HasIndex(x => x.CardNumberHash)
                    .IsUnique();

                entity.Property(x => x.CardNumberHash)
                    .IsUnicode(false);

                entity.HasOne(x => x.Customer)
                    .WithMany(x => x.VisaCards)
                    .HasForeignKey(x => x.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Account)
                    .WithMany(x => x.VisaCards)
                    .HasForeignKey(x => x.AccountId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =====================================
            // 14. VISA TRANSACTION LOG
            // =====================================
            modelBuilder.Entity<VisaTransactionLog>(entity =>
            {
                entity.ToTable("VisaTransactionLogs");

                entity.HasOne(x => x.VisaCard)
                    .WithMany(x => x.VisaTransactionLogs)
                    .HasForeignKey(x => x.VisaCardId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Transaction)
                    .WithMany()
                    .HasForeignKey(x => x.TransactionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
