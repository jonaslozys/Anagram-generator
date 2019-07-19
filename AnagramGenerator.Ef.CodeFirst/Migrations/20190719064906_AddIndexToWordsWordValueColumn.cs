using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.Ef.CodeFirst.Migrations
{
    public partial class AddIndexToWordsWordValueColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WordValue",
                table: "Words",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordValue",
                table: "Words",
                column: "WordValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Words_WordValue",
                table: "Words");

            migrationBuilder.AlterColumn<string>(
                name: "WordValue",
                table: "Words",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
