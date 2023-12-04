using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagerStarter.Migrations
{
    /// <inheritdoc />
    public partial class IsPrimaryDisplay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryDisplay",
                table: "EmailAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("3a406f64-ad7b-4098-ab01-7e93aae2b851"),
                column: "IsPrimaryDisplay",
                value: false);

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("3ddeb084-5e5d-4eca-b275-e4f6886e04dc"),
                column: "IsPrimaryDisplay",
                value: false);

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("5111f412-a7f4-4169-bb27-632687569ccd"),
                column: "IsPrimaryDisplay",
                value: false);

            migrationBuilder.UpdateData(
                table: "EmailAddresses",
                keyColumn: "Id",
                keyValue: new Guid("d1a50413-20c0-4972-a351-8be24e1fc939"),
                column: "IsPrimaryDisplay",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryDisplay",
                table: "EmailAddresses");
        }
    }
}
