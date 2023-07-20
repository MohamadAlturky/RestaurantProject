using Microsoft.EntityFrameworkCore;

namespace ScafoldingProject.Models;

public partial class RestaurantContext : DbContext
{
	public RestaurantContext()
	{
	}

	public RestaurantContext(DbContextOptions<RestaurantContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Customer> Customers { get; set; } = null!;
	public virtual DbSet<Meal> Meals { get; set; } = null!;
	public virtual DbSet<MealEntry> MealEntries { get; set; } = null!;
	public virtual DbSet<PricingRecord> PricingRecords { get; set; } = null!;
	public virtual DbSet<Reservation> Reservations { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("server=DESKTOP-OO326C9\\SQLEXPRESS;database= Restaurant;Trusted_Connection=True; Encrypt=False;");
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>(entity =>
		{
			entity.ToTable("Customer");

			entity.HasIndex(e => e.Id, "IX_Customer_Id")
				.IsUnique();

			entity.HasIndex(e => e.SerialNumber, "IX_Customer_SerialNumber")
				.IsUnique();
		});

		modelBuilder.Entity<Meal>(entity =>
		{
			entity.ToTable("Meal");

			entity.HasIndex(e => e.Id, "IX_Meal_Id")
				.IsUnique();
		});

		modelBuilder.Entity<MealEntry>(entity =>
		{
			entity.ToTable("MealEntry");

			entity.HasIndex(e => e.AtDay, "IX_MealEntry_AtDay");

			entity.HasIndex(e => e.Id, "IX_MealEntry_Id")
				.IsUnique();

			entity.HasIndex(e => e.MealId, "IX_MealEntry_MealId");

			entity.HasOne(d => d.Meal)
				.WithMany(p => p.MealEntries)
				.HasForeignKey(d => d.MealId);
		});

		modelBuilder.Entity<PricingRecord>(entity =>
		{
			entity.ToTable("PricingRecord");

			entity.HasIndex(e => e.Id, "IX_PricingRecord_Id")
				.IsUnique();
		});

		modelBuilder.Entity<Reservation>(entity =>
		{
			entity.ToTable("Reservation");

			entity.HasIndex(e => e.AtDay, "IX_Reservation_AtDay");

			entity.HasIndex(e => e.CustomerId, "IX_Reservation_CustomerId");

			entity.HasIndex(e => e.Id, "IX_Reservation_Id")
				.IsUnique();

			entity.HasIndex(e => e.MealEntryId, "IX_Reservation_MealEntryId");

			entity.HasOne(d => d.Customer)
				.WithMany(p => p.Reservations)
				.HasForeignKey(d => d.CustomerId);

			entity.HasOne(d => d.MealEntry)
				.WithMany(p => p.Reservations)
				.HasForeignKey(d => d.MealEntryId);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
