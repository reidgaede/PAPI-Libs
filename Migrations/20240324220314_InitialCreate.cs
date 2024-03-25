using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAPI_Libs.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PAPI_Libs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PAPI_LibTemplate = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalQuote = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalQuoteAuthorOrSource = table.Column<string>(type: "TEXT", nullable: false),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompletedString = table.Column<string>(type: "TEXT", nullable: true),
                    ApiUrlOne = table.Column<string>(type: "TEXT", nullable: false),
                    ApiNameOne = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUrlTwo = table.Column<string>(type: "TEXT", nullable: true),
                    ApiNameTwo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAPI_Libs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PAPI_LibTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TemplateString = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalQuote = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalQuoteAuthorOrSource = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUrlOne = table.Column<string>(type: "TEXT", nullable: false),
                    ApiNameOne = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUrlTwo = table.Column<string>(type: "TEXT", nullable: true),
                    ApiNameTwo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAPI_LibTemplates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAPI_Libs");

            migrationBuilder.DropTable(
                name: "PAPI_LibTemplates");
        }
    }
}
