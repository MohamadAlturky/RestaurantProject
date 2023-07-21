using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class ff : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
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
			name: "Permission",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Permission", x => x.Id);
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
			name: "Role",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Role", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "users",
			columns: table => new
			{
				Id = table.Column<long>(type: "bigint", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				CustomerId = table.Column<long>(type: "bigint", nullable: false),
				HiastMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
				HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
				RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_users", x => x.Id);
				table.ForeignKey(
					name: "FK_users_Customer_CustomerId",
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
			name: "RolePermission",
			columns: table => new
			{
				RoleId = table.Column<int>(type: "int", nullable: false),
				PermissionId = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
				table.ForeignKey(
					name: "FK_RolePermission_Permission_PermissionId",
					column: x => x.PermissionId,
					principalTable: "Permission",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_RolePermission_Role_RoleId",
					column: x => x.RoleId,
					principalTable: "Role",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "RoleUser",
			columns: table => new
			{
				RolesId = table.Column<int>(type: "int", nullable: false),
				UsersId = table.Column<long>(type: "bigint", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
				table.ForeignKey(
					name: "FK_RoleUser_Role_RolesId",
					column: x => x.RolesId,
					principalTable: "Role",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_RoleUser_users_UsersId",
					column: x => x.UsersId,
					principalTable: "users",
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

		migrationBuilder.InsertData(
			table: "Permission",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
					{ 1, "ReadMeals" },
					{ 2, "RegisterCustomer" },
					{ 3, "CreateMeal" }
			});

		migrationBuilder.InsertData(
			table: "Role",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
					{ 1, "Manager" },
					{ 2, "User" }
			});

		migrationBuilder.InsertData(
			table: "RolePermission",
			columns: new[] { "PermissionId", "RoleId" },
			values: new object[,]
			{
					{ 1, 1 },
					{ 2, 1 },
					{ 3, 1 },
					{ 1, 2 }
			});

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

		migrationBuilder.CreateIndex(
			name: "IX_Role_Id",
			table: "Role",
			column: "Id");

		migrationBuilder.CreateIndex(
			name: "IX_RolePermission_PermissionId",
			table: "RolePermission",
			column: "PermissionId");

		migrationBuilder.CreateIndex(
			name: "IX_RoleUser_UsersId",
			table: "RoleUser",
			column: "UsersId");

		migrationBuilder.CreateIndex(
			name: "IX_users_CustomerId",
			table: "users",
			column: "CustomerId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "PricingRecord");

		migrationBuilder.DropTable(
			name: "Reservation");

		migrationBuilder.DropTable(
			name: "RolePermission");

		migrationBuilder.DropTable(
			name: "RoleUser");

		migrationBuilder.DropTable(
			name: "MealEntry");

		migrationBuilder.DropTable(
			name: "Permission");

		migrationBuilder.DropTable(
			name: "Role");

		migrationBuilder.DropTable(
			name: "users");

		migrationBuilder.DropTable(
			name: "Meal");

		migrationBuilder.DropTable(
			name: "Customer");
	}
}
