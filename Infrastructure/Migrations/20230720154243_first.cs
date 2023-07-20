using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class first : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "AspNetRoles",
			columns: table => new
			{
				Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoles", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUsers",
			columns: table => new
			{
				Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
				UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
				PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
				SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
				TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
				LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
				LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
				AccessFailedCount = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUsers", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Customer",
			columns: table => new
			{
				Id = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				SerialNumber = table.Column<int>(type: "int", nullable: false),
				Balance = table.Column<int>(type: "int", nullable: false),
				FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
				LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
				BelongsToDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
				IsRegular = table.Column<bool>(type: "bit", nullable: false),
				Eligible = table.Column<bool>(type: "bit", nullable: false),
				IsActive = table.Column<bool>(type: "bit", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Customer", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Meal",
			columns: table => new
			{
				Id = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				NumberOfCalories = table.Column<int>(type: "int", nullable: false),
				Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
				ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Meal", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "PricingRecord",
			columns: table => new
			{
				Id = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Price = table.Column<int>(type: "int", nullable: false),
				CustomerTypeValue = table.Column<string>(type: "nvarchar(450)", nullable: false),
				MealTypeValue = table.Column<string>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_PricingRecord", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetRoleClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetUserClaims_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserLogins",
			columns: table => new
			{
				LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
				table.ForeignKey(
					name: "FK_AspNetUserLogins_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserRoles",
			columns: table => new
			{
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserTokens",
			columns: table => new
			{
				UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
				LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
				table.ForeignKey(
					name: "FK_AspNetUserTokens_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "ApplicationUser",
			columns: table => new
			{
				Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
				CustomerId = table.Column<long>(type: "bigint", nullable: false),
				UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
				NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
				Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
				NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
				EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
				PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
				SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
				TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
				LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
				LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
				AccessFailedCount = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ApplicationUser", x => x.Id);
				table.ForeignKey(
					name: "FK_ApplicationUser_Customer_CustomerId",
					column: x => x.CustomerId,
					principalTable: "Customer",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "MealEntry",
			columns: table => new
			{
				Id = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				MealId = table.Column<long>(type: "bigint", nullable: false),
				CustomerCanCancel = table.Column<bool>(type: "bit", nullable: false),
				AtDay = table.Column<DateTime>(type: "datetime2", nullable: false),
				ReservationsCount = table.Column<int>(type: "int", nullable: false),
				PreparedCount = table.Column<int>(type: "int", nullable: false),
				LastNumberInQueue = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_MealEntry", x => x.Id);
				table.ForeignKey(
					name: "FK_MealEntry_Meal_MealId",
					column: x => x.MealId,
					principalTable: "Meal",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "Reservation",
			columns: table => new
			{
				Id = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				AtDay = table.Column<DateTime>(type: "datetime2", nullable: false),
				CustomerId = table.Column<long>(type: "bigint", nullable: false),
				MealEntryId = table.Column<long>(type: "bigint", nullable: false),
				ReservationStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
				NumberInQueue = table.Column<int>(type: "int", nullable: false),
				Price = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Reservation", x => x.Id);
				table.ForeignKey(
					name: "FK_Reservation_Customer_CustomerId",
					column: x => x.CustomerId,
					principalTable: "Customer",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_Reservation_MealEntry_MealEntryId",
					column: x => x.MealEntryId,
					principalTable: "MealEntry",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_ApplicationUser_CustomerId",
			table: "ApplicationUser",
			column: "CustomerId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			table: "AspNetRoles",
			column: "NormalizedName",
			unique: true,
			filter: "[NormalizedName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "EmailIndex",
			table: "AspNetUsers",
			column: "NormalizedEmail");

		migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			table: "AspNetUsers",
			column: "NormalizedUserName",
			unique: true,
			filter: "[NormalizedUserName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_Customer_Id",
			table: "Customer",
			column: "Id",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_Customer_SerialNumber",
			table: "Customer",
			column: "SerialNumber",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_Meal_Id",
			table: "Meal",
			column: "Id",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_MealEntry_AtDay",
			table: "MealEntry",
			column: "AtDay");

		migrationBuilder.CreateIndex(
			name: "IX_MealEntry_Id",
			table: "MealEntry",
			column: "Id",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_MealEntry_MealId",
			table: "MealEntry",
			column: "MealId");

		migrationBuilder.CreateIndex(
			name: "IX_PricingRecord_Id",
			table: "PricingRecord",
			column: "Id",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_PricingRecord_MealTypeValue_CustomerTypeValue",
			table: "PricingRecord",
			columns: new[] { "MealTypeValue", "CustomerTypeValue" },
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_Reservation_AtDay",
			table: "Reservation",
			column: "AtDay");

		migrationBuilder.CreateIndex(
			name: "IX_Reservation_CustomerId",
			table: "Reservation",
			column: "CustomerId");

		migrationBuilder.CreateIndex(
			name: "IX_Reservation_Id",
			table: "Reservation",
			column: "Id",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_Reservation_MealEntryId",
			table: "Reservation",
			column: "MealEntryId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "ApplicationUser");

		migrationBuilder.DropTable(
			name: "AspNetRoleClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserLogins");

		migrationBuilder.DropTable(
			name: "AspNetUserRoles");

		migrationBuilder.DropTable(
			name: "AspNetUserTokens");

		migrationBuilder.DropTable(
			name: "PricingRecord");

		migrationBuilder.DropTable(
			name: "Reservation");

		migrationBuilder.DropTable(
			name: "AspNetRoles");

		migrationBuilder.DropTable(
			name: "AspNetUsers");

		migrationBuilder.DropTable(
			name: "Customer");

		migrationBuilder.DropTable(
			name: "MealEntry");

		migrationBuilder.DropTable(
			name: "Meal");
	}
}
