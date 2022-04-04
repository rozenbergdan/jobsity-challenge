using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge.Infrastructure.Migrations
{
    public partial class AddUserBot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c7a3320-0d7f-4b8a-88d7-93d855462f90",
                column: "ConcurrencyStamp",
                value: "003d7273-5aeb-45ae-9109-0f6722a8380c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21397611-90c1-4359-94d0-0800eb2a4f5b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "789e1dce-9a23-44bb-8eea-9bac0b7c159c", "AQAAAAEAACcQAAAAEAXkg1qfzq5RuHhprF7Jg81haI0uLKjam8Y72XwTtVlbwfwAnJdfxMoEVY8WS6Gj0Q==", "9079f8b4-9a1f-4f0c-8d2a-e7d41f29f2ff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25caed64-ae6c-4069-bb72-554ad038498e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c499844-1447-4507-8329-da6e4e1b56fb", "AQAAAAEAACcQAAAAEAm+MPb00Xu2FT0uAqFTYyMdhV83XYL+iD8wE2KIEA9xs+XQIFwPdDowVIbMuiFTxA==", "66d7a02a-2e11-4f61-9caa-25d8f19aa8c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c09e1e40-dc1f-45b7-8b52-b65145954d94",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e218c298-2258-41d5-ae0f-15f14c59bcd5", "AQAAAAEAACcQAAAAEIIDBvouj3f+orDVNYwJrx+vDL1J2nD0shV4Ewv1KbDxDPMrazsxaVasx2oxNGAaYw==", "7627d88b-99e7-4569-8fe2-63a9d30becba" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9efc17fe-738a-4b4c-bdb6-002d17605b7c", 0, "28c12f66-0536-4581-979e-5fa2703254c6", null, false, false, null, null, "STOCK_BOT", "AQAAAAEAACcQAAAAEHMC3pAPaST1DOlRGP1COIxv+0WbGFBKbpYd1141W1vpYbIJOa21Iz4QQlK4yBntPA==", null, false, "847e0216-afc1-47a6-b1bb-8d8438413172", false, "stock_bot" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c7a3320-0d7f-4b8a-88d7-93d855462f90", "9efc17fe-738a-4b4c-bdb6-002d17605b7c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c7a3320-0d7f-4b8a-88d7-93d855462f90", "9efc17fe-738a-4b4c-bdb6-002d17605b7c" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9efc17fe-738a-4b4c-bdb6-002d17605b7c");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c7a3320-0d7f-4b8a-88d7-93d855462f90",
                column: "ConcurrencyStamp",
                value: "6a5f9d98-6ac2-4937-95b6-16121b272a30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21397611-90c1-4359-94d0-0800eb2a4f5b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d41c1b17-b040-4bcc-885b-5a51cd04dbaf", "AQAAAAEAACcQAAAAEBTlhqcVj0k7SPg197nh509DSNbXwB1f/X8hYJ2aFR2+pLqacx2tRe4pJWkHZEf7Yw==", "6c2c0079-dbe0-419e-aa02-baf9e99e057c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25caed64-ae6c-4069-bb72-554ad038498e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f7f3b20-cc62-4c7e-ab80-4254da2d8b84", "AQAAAAEAACcQAAAAEIMCXmszXO8Z0am9JV73I0WKTF2mx85Zict+LMq+IlK8ANnpqsv76fesOGCgDxx3sQ==", "772c0fe4-45a8-4bd5-baf4-f26c25640df1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c09e1e40-dc1f-45b7-8b52-b65145954d94",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dacf5aeb-e43f-42c4-a7b2-c0676ae43301", "AQAAAAEAACcQAAAAEKCiLuWP5DVNMGsJVPUA8+R3P0yO0eYovB7qg2ei4GX2tBrn7DI77tDhGW+Nyh8IYA==", "251327fb-9abe-405b-b4f4-f0273693df45" });
        }
    }
}
