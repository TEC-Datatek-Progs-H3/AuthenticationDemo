using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthenticationDemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedHeroTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    RealName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    DebutYear = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hero", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Hero",
                columns: new[] { "Id", "DebutYear", "HeroName", "Place", "RealName" },
                values: new object[,]
                {
                    { 1, (short)1938, "Superman", "Metropolis", "Clark Kent" },
                    { 2, (short)1963, "Iron Man", "Malibu", "Tony Stark" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hero");
        }
    }
}
