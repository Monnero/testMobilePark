using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testMobilePark.Migrations
{
    /// <inheritdoc />
    public partial class upd_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FragmentType",
                table: "News",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VowelCount",
                table: "News",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FragmentType",
                table: "News");

            migrationBuilder.DropColumn(
                name: "VowelCount",
                table: "News");
        }
    }
}
