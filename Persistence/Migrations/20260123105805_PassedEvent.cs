using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PassedEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PassedEventParticipants_HrmId",
                table: "PassedEventParticipants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PassedEventParticipants");

            migrationBuilder.CreateIndex(
                name: "IX_PassedEventParticipants_HrmId",
                table: "PassedEventParticipants",
                column: "HrmId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PassedEventParticipants_HrmId",
                table: "PassedEventParticipants");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PassedEventParticipants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PassedEventParticipants_HrmId",
                table: "PassedEventParticipants",
                column: "HrmId",
                unique: true);
        }
    }
}
