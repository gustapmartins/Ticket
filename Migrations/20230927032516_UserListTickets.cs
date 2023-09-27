using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Migrations
{
    /// <inheritdoc />
    public partial class UserListTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Tickets",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UsersId",
                table: "Tickets",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_UsersId",
                table: "Tickets",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_UsersId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UsersId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Tickets");
        }
    }
}
