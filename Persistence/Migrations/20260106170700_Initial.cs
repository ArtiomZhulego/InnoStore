using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HrmId = table.Column<int>(type: "integer", nullable: false),
                    FirstNameRU = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    PatronymicRU = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastNameRU = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FirstNameEN = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    PatronymicEN = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    LastNameEN = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    OfficeId = table.Column<int>(type: "integer", nullable: true),
                    JobTitleId = table.Column<Guid>(type: "uuid", nullable: true),
                    LinkProfilePictureMini = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HrmId",
                table: "Users",
                column: "HrmId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
