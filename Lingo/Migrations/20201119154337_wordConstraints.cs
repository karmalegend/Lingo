using Microsoft.EntityFrameworkCore.Migrations;

namespace Lingo.Migrations
{
    public partial class wordConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Word",
                table: "sevenLetterWords",
                newName: "word");

            migrationBuilder.AlterColumn<string>(
                name: "word",
                table: "sixLetterWords",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "word",
                table: "sevenLetterWords",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "word",
                table: "fiveLetterWords",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sixLetterWords_word",
                table: "sixLetterWords",
                column: "word",
                unique: true,
                filter: "[word] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_sevenLetterWords_word",
                table: "sevenLetterWords",
                column: "word",
                unique: true,
                filter: "[word] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_fiveLetterWords_word",
                table: "fiveLetterWords",
                column: "word",
                unique: true,
                filter: "[word] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_sixLetterWords_word",
                table: "sixLetterWords");

            migrationBuilder.DropIndex(
                name: "IX_sevenLetterWords_word",
                table: "sevenLetterWords");

            migrationBuilder.DropIndex(
                name: "IX_fiveLetterWords_word",
                table: "fiveLetterWords");

            migrationBuilder.RenameColumn(
                name: "word",
                table: "sevenLetterWords",
                newName: "Word");

            migrationBuilder.AlterColumn<string>(
                name: "word",
                table: "sixLetterWords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "sevenLetterWords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "word",
                table: "fiveLetterWords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5,
                oldNullable: true);
        }
    }
}
