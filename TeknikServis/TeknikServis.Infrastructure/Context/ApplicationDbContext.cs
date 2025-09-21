using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;
using TeknikServis.Domain.ValueObjects;

namespace TeknikServis.Infrastructure.Context;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

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

        // Address as owned type on Customer
        builder.Entity<Customer>().OwnsOne(c => c.Address, a =>
        {
            a.Property(p => p.AddressLine).HasColumnName("AddressLine");
            a.Property(p => p.City).HasColumnName("City");
            a.Property(p => p.Neighborhood).HasColumnName("Neighborhood");
            a.Property(p => p.District).HasColumnName("District");
            a.Property(p => p.ZipCode).HasColumnName("ZipCode");
            a.Property(p => p.Country).HasColumnName("Country");
        });

        // SmartEnum conversions
        builder.Entity<Customer>()
            .Property(c => c.CustomerType)
            .HasConversion(
                v => v.Name,
                v => CustomerType.FromName(v, false)
            );

        builder.Entity<Product>()
            .Property(p => p.ProductType)
            .HasConversion(
                v => v.Name,
                v => ProductType.FromName(v, false)
            );

        builder.Entity<Person>()
            .Property(p => p.ExpertiseArea)
            .HasConversion(
                v => v.Name,
                v => ExpertiseArea.FromName(v, false)
            );

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

        // Ignore Identity tables not used
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
    }
}
