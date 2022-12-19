using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class UserNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_UserIDId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "UserIDId",
                table: "Assignments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_UserIDId",
                table: "Assignments",
                newName: "IX_Assignments_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_UserId",
                table: "Assignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_UserId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assignments",
                newName: "UserIDId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_UserId",
                table: "Assignments",
                newName: "IX_Assignments_UserIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_UserIDId",
                table: "Assignments",
                column: "UserIDId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
