using BANK_APP.Server.Model;
using Microsoft.EntityFrameworkCore;


namespace BANK_APP.Server.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options, IConfiguration config ) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            string connectionString = _config.GetConnectionString("DefaultConnection");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany<Role>(e => e.Roles)
                .WithMany(e => e.Accounts)
                .UsingEntity<Account_Roles>(

                    l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.Role_Id),
                    r => r.HasOne<Account>().WithMany().HasForeignKey(e => e.Account_Id) );

            modelBuilder.Entity<Role>()
                .HasMany<Permission>(e => e.Permissions)
                .WithMany(e => e.Roles)
                .UsingEntity<Role_Permissions>(
                    l => l.HasOne<Permission>().WithMany().HasForeignKey(e => e.Permission_Id),
                    r => r.HasOne<Role>().WithMany().HasForeignKey(e => e.Role_Id) );
        }

        public DbSet<Account> ACCOUNT { get; set; }
        public DbSet<Role> ROLE { get; set; }
        public DbSet<Permission> PERMISSION { get; set; }
    }
}
