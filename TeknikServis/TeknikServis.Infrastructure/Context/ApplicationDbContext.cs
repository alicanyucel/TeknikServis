using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Infrastructure.Context;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid,
    IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Neighbourhood> Neighbourhoods => Set<Neighbourhood>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<ServiceAction> ServiceActions => Set<ServiceAction>();
    public DbSet<ServiceLineAction> ServiceLineActions => Set<ServiceLineAction>();
    public DbSet<DocumentLink> DocumentLinks => Set<DocumentLink>();
    public DbSet<VideoLink> VideoLinks => Set<VideoLink>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // AppUser yapılandırması
        builder.Entity<AppUser>(b =>
        {
            b.ToTable("AppUsers");

            b.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(u => u.RefreshToken)
                .HasMaxLength(500);

            b.Property(u => u.RefreshTokenExpires);
        });
        //ulke içe il mahalle
        builder.Entity<Country>()
       .HasMany(c => c.Provinces)
       .WithOne(p => p.Country)
       .HasForeignKey(p => p.CountryId)
       .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Province>()
            .HasMany(p => p.Districts)
            .WithOne(d => d.Province)
            .HasForeignKey(d => d.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<District>()
            .HasMany(d => d.Neighbourhoods)
            .WithOne(n => n.District)
            .HasForeignKey(n => n.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<District>()
            .HasIndex(d => d.PostalCode)
            .IsUnique(false);
        builder.Entity<AppRole>(b =>
        {
            b.ToTable("AppRoles");

            b.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();
        });

        // AppUserRole (User-Role join) configuration
        builder.Entity<AppUserRole>(b =>
        {
            b.ToTable("AppUserRoles");
            b.HasKey(ur => new { ur.UserId, ur.RoleId });

            b.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });
        //prouct status
             builder.Entity<Status>()
            .HasOne(s => s.Product)
            .WithMany(p => p.StatusHistory)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Diğer Identity tabloları
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        // Address as owned type on Customer
        builder.Entity<Customer>().OwnsOne(c => c.Address, a =>
        {
            a.Property(p => p.AddressLine).HasColumnName("AddressLine");
        });

        // SmartEnum conversions
        builder.Entity<Customer>()
            .Property(c => c.CustomerType)
            .HasConversion(v => v.Name, v => CustomerType.FromName(v, false));

        var productTypeConverter = new ValueConverter<ProductType, string>(
            v => v.Name,
            v => ProductTypeFromName(v)
        );

        builder.Entity<Product>()
            .Property(p => p.ProductType)
            .HasConversion(productTypeConverter);

        builder.Entity<Person>()
            .Property(p => p.ExpertiseArea)
            .HasConversion(v => v.Name, v => ExpertiseArea.FromName(v, false));

        // ServiceLineAction relationships
        builder.Entity<ServiceLineAction>(b =>
        {
            b.HasOne(sla => sla.Product)
                .WithMany()
                .HasForeignKey(sla => sla.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(sla => sla.ServiceAction)
                .WithMany(sa => sa.ServiceLineActions)
                .HasForeignKey(sla => sla.ServiceActionId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(sla => sla.Person)
                .WithMany()
                .HasForeignKey(sla => sla.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(sla => sla.Customer)
                .WithMany()
                .HasForeignKey(sla => sla.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(sla => sla.Status)
                .WithMany()
                .HasForeignKey(sla => sla.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ServiceAction relationships
        builder.Entity<ServiceAction>(b =>
        {
            b.HasOne(sa => sa.Person)
                .WithMany(p => p.Actions)
                .HasForeignKey(sa => sa.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(sa => sa.Status)
                .WithMany()
                .HasForeignKey(sa => sa.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(sa => sa.DocumentLinks)
                .WithOne(dl => dl.ServiceAction)
                .HasForeignKey(dl => dl.ServiceActionId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(sa => sa.VideoLinks)
                .WithOne(vl => vl.ServiceAction)
                .HasForeignKey(vl => vl.ServiceActionId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(sa => sa.Customer)
                .WithMany()
                .HasForeignKey(sa => sa.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Product → Customer
        builder.Entity<Product>(b =>
        {
            b.HasOne(p => p.Customer)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Apply configurations
        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }

    private static ProductType ProductTypeFromName(string name)
    {
        if (ProductType.TryFromName(name, true, out var pt))
        {
            return pt;
        }
        return ProductType.Accessory;
    }
}
