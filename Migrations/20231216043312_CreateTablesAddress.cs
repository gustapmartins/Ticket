using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Shows_ShowId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "Address",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                table: "Shows",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_AddressId",
                table: "Shows",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Address_AddressId",
                table: "Shows",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Address_AddressId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_AddressId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Address",
                newName: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Shows_ShowId",
                table: "Address",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
