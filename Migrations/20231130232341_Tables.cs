using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UsersId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Carts_CartId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CartId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "Tickets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UsersId",
                table: "Carts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UsersId",
                table: "Carts",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UsersId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "CartId",
                table: "Tickets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartId1",
                table: "Tickets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UsersId",
                table: "Carts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CartId1",
                table: "Tickets",
                column: "CartId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UsersId",
                table: "Carts",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Carts_CartId",
                table: "Tickets",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Carts_CartId1",
                table: "Tickets",
                column: "CartId1",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
