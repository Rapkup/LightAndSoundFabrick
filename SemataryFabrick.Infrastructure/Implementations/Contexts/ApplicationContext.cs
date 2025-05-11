using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.CartModels;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Infrastructure.Implementations.Contexts;
public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderBase> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderCrew> OrderCrews { get; set; }
    public DbSet<WorkTask> WorkTasks { get; set; }
    public DbSet<WorkTaskAssignment> WorkTaskAssignments { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemInventory> Inventories { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresEnum<OrderType>();
        modelBuilder.HasPostgresEnum<OrderState>();
        modelBuilder.HasPostgresEnum<PaymentStatus>();
        modelBuilder.HasPostgresEnum<ProductState>();
        modelBuilder.HasPostgresEnum<UserType>();
        modelBuilder.HasPostgresEnum<WorkTaskState>();

        modelBuilder.Entity<Cart>(e =>
       {
           e.HasKey(c => c.Id);
           e.HasOne(c => c.Customer)
               .WithMany()
               .HasForeignKey(c => c.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
       });
        // Конфигурация CartItem
        modelBuilder.Entity<CartItem>(e =>
        {
            e.HasKey(ci => ci.Id);
            
            e.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId);

            e.HasOne(ci => ci.Product)
                .WithMany(i => i.CartItems)
                .HasForeignKey(ci => ci.ProductId);
                
            e.HasOne(ci => ci.Discount)
                .WithMany()
                .HasForeignKey(ci => ci.DiscountId)
                .IsRequired(false); 
        });
        // Конфигурация OrderBase
        modelBuilder.Entity<OrderBase>(e =>
        {
            e.HasKey(o => o.Id);

            e.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(o => o.OrderManager)
                .WithMany(om => om.OrderBases)
                .HasForeignKey(o => o.OrderManagerId);

            e.HasOne(o => o.TechOrderLead)
                .WithMany(tol => tol.OrderBases)
                .HasForeignKey(o => o.TechOrderLeadId);

            e.HasMany(o => o.OrderItems)
                .WithOne(i => i.OrderBase)
                .HasForeignKey(i => i.OrderBaseId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(o => o.OrderCrews)
                .WithOne(c => c.OrderBase)
                .HasForeignKey(c => c.OrderBaseId);
        });
        // Конфигурация OrderItem
          modelBuilder.Entity<OrderItem>(e =>
        {
            e.HasKey(oi => oi.Id);
            
            e.HasOne(oi => oi.OrderBase)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderBaseId);

            e.HasOne(oi => oi.Product)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
                
            e.HasOne(oi => oi.Discount)
                .WithMany()
                .HasForeignKey(oi => oi.DiscountId)
                .IsRequired(false); 
        });
        // Конфигурация OrderCrew
        modelBuilder.Entity<OrderCrew>(e =>
        {
            e.HasKey(oc => oc.Id);

            e.HasOne(oc => oc.TechOrderLead)
                .WithMany(tol => tol.OrderCrews)
                .HasForeignKey(oc => oc.TechLeadId);

            e.HasOne(oc => oc.OrderBase)
                .WithMany(o => o.OrderCrews)
                .HasForeignKey(oc => oc.OrderBaseId);

            e.HasMany(oc => oc.WorkTaskAssignments)
                .WithOne(wta => wta.OrderCrew)
                .HasForeignKey(wta => wta.OrderCrewId);

            e.HasMany(oc => oc.Workers)
                .WithMany(w => w.OrderCrews);
        });
        // Конфигурация пользователей (TPH)
        modelBuilder.Entity<ApplicationUser>(e =>
        {
            e.ToTable("Users");
            e.HasKey(u => u.Id);

            e.HasDiscriminator(u => u.UserType)
                .HasValue<IndividualCustomer>(UserType.IndividualCustomer)
                .HasValue<LegalCustomer>(UserType.LegalCustomer)
                .HasValue<OrderManager>(UserType.OrderManager)
                .HasValue<TechOrderLead>(UserType.TechOrderLead)
                .HasValue<Worker>(UserType.Worker)
                .HasValue<Director>(UserType.Director);
        });
        modelBuilder.Entity<IndividualCustomer>(e =>
        {
            e.Property(ic => ic.PassportIdNumber)
                .HasMaxLength(50);

            //e.HasMany(ic => ic.OrderBases)
            //    .WithOne(o => (IndividualCustomer)o.Customer)
            //    .HasForeignKey(o => o.CustomerId)
            //    .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<LegalCustomer>(e =>
        {
            //e.HasMany(lc => lc.OrderBases)
            //     .WithOne(o => (LegalCustomer)o.Customer)
            //     .HasForeignKey(o => o.CustomerId)
            //     .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<OrderManager>(e =>
        {
            e.HasMany(om => om.OrderBases)
                .WithOne(o => o.OrderManager)
                .HasForeignKey(o => o.OrderManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<TechOrderLead>(e =>
        {
            e.HasMany(tol => tol.OrderBases)
                .WithOne(o => o.TechOrderLead)
                .HasForeignKey(o => o.TechOrderLeadId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(tol => tol.OrderCrews)
                .WithOne(oc => oc.TechOrderLead)
                .HasForeignKey(oc => oc.TechLeadId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Worker>(e =>
        {
            e.HasMany(w => w.OrderCrews)
                .WithMany(oc => oc.Workers);
        });
        // Конфигурация Item
        modelBuilder.Entity<Item>(e =>
        {
            e.HasKey(i => i.Id);

            e.HasOne(i => i.Inventory)
                .WithMany(inv => inv.Items)
                .HasForeignKey(i => i.InventoryId);

            e.HasOne(i => i.SubCategory)
                .WithMany(sc => sc.Items)
                .HasForeignKey(i => i.SubCategoryId);
        });
        modelBuilder.Entity<WorkTask>(e =>
        {
            e.HasKey(wt => wt.Id);

            e.HasMany(wt => wt.WorkTaskAssignments)
                .WithOne(wta => wta.WorkTask)
                .HasForeignKey(wta => wta.WorkTaskId);
        });
        // Конфигурация WorkTaskAssignment
        modelBuilder.Entity<WorkTaskAssignment>(e =>
        {
            e.HasKey(wta => wta.Id);
            e.HasOne(wta => wta.WorkTask)
                .WithMany(wt => wt.WorkTaskAssignments)
                .HasForeignKey(wta => wta.WorkTaskId);

            e.HasOne(wta => wta.OrderCrew)
                .WithMany(oc => oc.WorkTaskAssignments)
                .HasForeignKey(wta => wta.OrderCrewId);
        });
        modelBuilder.Entity<ProductCategory>(e =>
        {
            e.HasKey(pc => pc.Id);
            e.HasMany(pc => pc.SubCategories)
                .WithOne(sc => sc.ParentCategory)
                .HasForeignKey(sc => sc.ParentCategoryId);
        });
    }
}