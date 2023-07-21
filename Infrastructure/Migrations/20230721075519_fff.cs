using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class fff : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "RoleUser");

		migrationBuilder.AddColumn<int>(
			name: "SerialNumber",
			table: "users",
			type: "int",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.CreateTable(
			name: "UserRole",
			columns: table => new
			{
				UserId = table.Column<long>(type: "bigint", nullable: false),
				RoleId = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
				table.ForeignKey(
					name: "FK_UserRole_Role_RoleId",
					column: x => x.RoleId,
					principalTable: "Role",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_UserRole_users_UserId",
					column: x => x.UserId,
					principalTable: "users",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_users_SerialNumber",
			table: "users",
			column: "SerialNumber",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_UserRole_UserId",
			table: "UserRole",
			column: "UserId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "UserRole");

		migrationBuilder.DropIndex(
			name: "IX_users_SerialNumber",
			table: "users");

		migrationBuilder.DropColumn(
			name: "SerialNumber",
			table: "users");

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

		migrationBuilder.CreateIndex(
			name: "IX_RoleUser_UsersId",
			table: "RoleUser",
			column: "UsersId");
	}
}
