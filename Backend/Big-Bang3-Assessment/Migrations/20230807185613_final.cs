using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Big_Bang3_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "amount_for_childer",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "amount_for_person",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "no_of_childer",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "no_of_perons",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "adminRegisters",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "adminRegisters",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount_for_childer",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "amount_for_person",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "no_of_childer",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "no_of_perons",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "adminRegisters");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "adminRegisters");
        }
    }
}
