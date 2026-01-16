using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PassedEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassedEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EventType = table.Column<int>(type: "integer", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassedEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassedEventParticipants",
                columns: table => new
                {
                    HrmId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    PassedEventId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassedEventParticipants", x => x.HrmId);
                    table.ForeignKey(
                        name: "FK_PassedEventParticipants_PassedEvents_PassedEventId",
                        column: x => x.PassedEventId,
                        principalTable: "PassedEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassedEventParticipants_HrmId",
                table: "PassedEventParticipants",
                column: "HrmId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassedEventParticipants_PassedEventId",
                table: "PassedEventParticipants",
                column: "PassedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_PassedEvents_Id",
                table: "PassedEvents",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassedEventParticipants");

            migrationBuilder.DropTable(
                name: "PassedEvents");
        }
    }
}
